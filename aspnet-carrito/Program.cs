var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. Registrar el almacenamiento en memoria para sesiones
builder.Services.AddDistributedMemoryCache();

// 2. Configurar la sesión con cookie segura
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;                          // flag HttpOnly
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // flag Secure
    options.Cookie.SameSite = SameSiteMode.Strict;            // flag SameSite
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 3. Habilitar el middleware de sesión (ANTES de MapControllers)
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Carrito}/{action=Index}/{id?}");

app.Run();