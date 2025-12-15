🏥 PT Detecta - API Backend

API RESTful robusta y escalable para la gestión de pacientes e historiales clínicos. Desarrollada con .NET 9 siguiendo los principios de Clean Architecture (Onion).

🔗 URL Producción: https://backendptdetecta.onrender.com

🏗️ Arquitectura

El sistema sigue un diseño de Monolito Modular dividido en capas:

Domain: Entidades y Lógica de Negocio (Sin dependencias).

Application: Casos de uso, DTOs e Interfaces.

Infrastructure: EF Core, Repositorios, Identity y Servicios Externos.

API: Controladores REST y configuración de dependencias.

⚙️ Configuración Local

Prerrequisitos

.NET SDK 9.0

PostgreSQL

1. Configurar Base de Datos

Actualiza el archivo appsettings.json con tu cadena de conexión local:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=DetectaDB;Username=postgres;Password=tu_password"
}


2. Ejecutar Migraciones

Desde la terminal en la carpeta del proyecto:

dotnet ef database update


3. Ejecutar la Aplicación

dotnet watch run


La API estará disponible en http://localhost:5000. Swagger en /swagger.

☁️ Despliegue en Producción (Render)

El proyecto utiliza Docker. Para desplegar en Render, configura las siguientes Variables de Entorno:

Clave (Render)

Valor / Notas

ConnectionStrings__DefaultConnection

Cadena de Neon.tech (debe incluir al final: ;SSL Mode=Require;Trust Server Certificate=true).

Jwt__Key

Clave secreta fuerte para firmar tokens.

Jwt__Issuer

URL del backend (https://backendptdetecta.onrender.com).

Jwt__Audience

URL del frontend (Vercel).

ASPNETCORE_ENVIRONMENT

Production

Desarrollado con ❤️ por Gherson Alexis