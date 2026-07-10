# Gestión de Estado: Sesiones y Carrito de Compras

Implementación comparativa de un carrito de compras usando **sesiones de servidor** en tres plataformas: **PHP 8**, **ASP.NET Core 8** y **Java / Spring Boot 3**.

Aplicaciones Web — Ingeniería de Software — UTEQ 2025-2026
GA — Práctica en clase | Formativa | Individual

## Qué se construyó

Un carrito de compras mínimo con tres operaciones sobre una sesión del servidor, implementado tres veces (una por plataforma):

- **Agregar** un producto (nombre + precio) al carrito guardado en la sesión.
- **Eliminar** un producto por su índice en el carrito.
- **Limpiar** el carrito completo (vaciar o destruir la sesión).

## Estructura del repositorio

gestion-estado-sesiones-carrito-compras/
├── php-carrito/          # PHP 8 con $_SESSION
├── aspnet-carrito/        # ASP.NET Core 8 con ISession
├── spring-carrito/        # Java / Spring Boot 3 con HttpSession
├── docs/
│   ├── informe_sesiones_carrito.pdf   # Informe completo con evidencias
│   └── capturas/                       # Capturas de pantalla y DevTools
├── .gitignore
└── README.md

## Instrucciones de ejecución

### PHP 8

1. Servir la carpeta `php-carrito/` con Apache (XAMPP) o Docker:

docker run -d -p 8080:80 php:8.2-apache

2. Abrir en el navegador:

http://localhost/carrito/index.php

> Nota: el flag `Secure` de la cookie está comentado en `configuracion.php` para permitir pruebas en `http://localhost` sin HTTPS. En producción debe activarse.

### ASP.NET Core 8

1. Entrar a la carpeta del proyecto:

cd aspnet-carrito

2. El archivo `global.json` fija el SDK en la versión 8, aunque el equipo tenga instalada una versión más reciente.
3. Ejecutar con el perfil HTTPS (necesario porque la cookie de sesión exige `Secure`):

dotnet run --launch-profile https

4. Abrir en el navegador la URL HTTPS que muestre la terminal, agregando `/Carrito`, por ejemplo:

https://localhost:7074/Carrito

### Java / Spring Boot 3

1. Entrar a la carpeta del proyecto:

cd spring-carrito

2. Ejecutar con el Maven Wrapper (no requiere tener Maven instalado globalmente):

./mvnw spring-boot:run

3. Abrir en el navegador:

http://localhost:8080/carrito

## Flags de seguridad de cookies verificados

En las tres plataformas se configuraron y verificaron en DevTools los tres flags de seguridad de la cookie de sesión:

- **HttpOnly**: impide que JavaScript del cliente acceda a la cookie (previene robo de sesión vía XSS).
- **Secure**: la cookie solo se envía por HTTPS (previene interceptación en redes inseguras).
- **SameSite=Strict**: la cookie no se envía en solicitudes originadas desde otros sitios (previene CSRF).

## Por qué el PFC usa JWT en lugar de sesiones de servidor

Las sesiones de servidor son adecuadas para aplicaciones web tradicionales multi-página, como la implementada en esta práctica. El PFC, en cambio, usa una SPA en Angular que consume una API REST de Spring Boot, por lo que reemplaza las sesiones por **JWT almacenado en cookie HttpOnly**: mantiene la arquitectura REST sin estado en el servidor, escala horizontalmente sin configuración adicional entre instancias, y protege el token contra ataques XSS.

El detalle completo de esta comparación, la tabla de las tres plataformas y las respuestas a las preguntas de reflexión están en [`docs/informe_sesiones_carrito.pdf`](docs/informe_sesiones_carrito.pdf).

## Flujo de Git/GitHub utilizado

Se trabajó directamente sobre `main`, con un commit por avance significativo:

1. `chore`: estructura de carpetas y `.gitignore` para las tres plataformas.
2. `feat(php)`: carrito de compras con sesiones en PHP 8.
3. `fix(php)`: ajuste del flag `Secure` para pruebas en localhost sin HTTPS.
4. `feat(aspnet)`: proyecto MVC base con .NET 8 forzado vía `global.json`.
5. `feat(aspnet)`: carrito de compras con `ISession` y flags de seguridad.
6. `feat(springboot)`: carrito de compras con `HttpSession` y flags de seguridad en `application.yaml`.
7. `docs`: informe con evidencias y capturas de las tres plataformas.

## Autor

Panamá Murillo Moisés Antonio — Aplicaciones Web, Ingeniería de Software, UTEQ 2025-2026