# PT Detecta – Backend (API REST)

Backend API para la gestión clínica básica:
- Registro e inicio de sesión de usuarios (ASP.NET Core Identity + JWT).
- Gestión de **pacientes** (listar, crear, editar, eliminación lógica y re-habilitar).
- Gestión de **historial clínico** asociado a pacientes.
- Catálogo de **tipos de seguro**.
- Auditoría automática (usuario/fechas) en altas, modificaciones y eliminación lógica.

> Nota: este repositorio contiene un único proyecto .NET (`BackendPTDetecta.csproj`) con separación por carpetas (estilo Clean Architecture/Onion).

## Demo / despliegue
Si el proyecto está desplegado, la URL usada en este repositorio es:
- https://backendptdetecta.onrender.com

## Tecnologías
- .NET 9 (ASP.NET Core Web API)
- Entity Framework Core 9 (Code First + migraciones)
- PostgreSQL (Npgsql EF Core Provider)
- ASP.NET Core Identity
- JWT Bearer Authentication
- Docker (Dockerfile incluido)

## Arquitectura (Clean / Onion por capas)
El código está organizado por capas para separar responsabilidades:

- `Domain/`: entidades y reglas de negocio (núcleo).
- `Application/`: DTOs e interfaces (contratos) que definen los casos de uso/servicios.
- `Infrastructure/`: persistencia EF Core, repositorios e implementación de servicios.
- `Controllers/`: capa de presentación (endpoints HTTP).

Además, existe un interceptor de EF Core para auditoría:
- `Infrastructure/Persistence/Interceptors/AuditoriaInterceptor.cs`

## Endpoints principales
Base path: `/api`

- Autenticación:
  - `POST /api/Auth/register`
  - `POST /api/Auth/login`
  - `POST /api/Auth/forgot-password`
  - `POST /api/Auth/reset-password`
- Pacientes:
  - `GET /api/Pacientes`
  - `GET /api/Pacientes/{id}`
  - `POST /api/Pacientes`
  - `PUT /api/Pacientes/{id}`
  - `PUT /api/Pacientes/eliminar/{id}`
  - `GET /api/Pacientes/buscar-eliminado/{dni}`
  - `PUT /api/Pacientes/habilitar/{id}`
- Historial clínico:
  - `GET /api/HistorialClinico/paciente/{idPaciente}`
  - `PUT /api/HistorialClinico/paciente/{idPaciente}`
- Tipos de seguro:
  - `GET /api/TiposSeguro`

## Ejecutar en local (guía para un usuario nuevo)

### Prerrequisitos
- .NET SDK 9
- PostgreSQL 16 (local o Docker)

### 1) Clonar
```bash
git clone https://github.com/galexxmc/BackendPTDetecta.git
cd BackendPTDetecta
```

### 2) Configurar la base de datos
La API espera una conexión PostgreSQL en `ConnectionStrings:DefaultConnection`.

Opción A: PostgreSQL con Docker
```bash
docker run --name pt-detecta-postgres -d \
  -e POSTGRES_DB=DetectaDB \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -p 5432:5432 postgres:16
```

Opción B: PostgreSQL instalado localmente
- Crea una base de datos, por ejemplo: `DetectaDB`
- Asegura un usuario/contraseña con permisos sobre esa DB

### 3) Configuración (appsettings)
La configuración vive en `appsettings.json`.

Para desarrollo local se recomienda **NO** commitear credenciales reales. Puedes:
- Editar `appsettings.json` localmente, o
- Crear/ajustar `appsettings.Development.json` con tus valores.

Ejemplo (recomendado) de `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=DetectaDB;Username=postgres;Password=postgres"
  },
  "Jwt": {
    "Key": "REEMPLAZA_POR_UN_SECRETO_LARGO",
    "Issuer": "DetectaApi",
    "Audience": "DetectaFront"
  }
}
```

Alternativa: variables de entorno (útil en Docker/CI)
- `ConnectionStrings__DefaultConnection`
- `Jwt__Key`
- `Jwt__Issuer`
- `Jwt__Audience`

### 4) Restaurar dependencias
```bash
dotnet restore
```

### 5) Aplicar migraciones (crear esquema en la DB)
Este repositorio ya incluye migraciones en `Migrations/`.

1) Instala la herramienta EF (si no la tienes):
```bash
dotnet tool install --global dotnet-ef
```

2) Ejecuta las migraciones:
```bash
dotnet ef database update
```

### 6) Ejecutar la API
```bash
dotnet run
```

Puertos por defecto (según `Properties/launchSettings.json`):
- HTTP: `http://localhost:5036`
- HTTPS: `https://localhost:7278`

### 7) Probar rápidamente
Hay un archivo de ejemplo: `BackendPTDetecta.http`.

También puedes probar (ejemplo):
- `GET http://localhost:5036/api/TiposSeguro`

## Ejecutar con Docker (opcional)
Construir imagen:
```bash
docker build -t pt-detecta-backend .
```

Correr contenedor (la imagen expone `8080`):
```bash
docker run --rm -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Port=5432;Database=DetectaDB;Username=postgres;Password=postgres" \
  -e Jwt__Key="REEMPLAZA_POR_UN_SECRETO_LARGO" \
  -e Jwt__Issuer="DetectaApi" \
  -e Jwt__Audience="DetectaFront" \
  pt-detecta-backend
```

## Estructura del proyecto (resumen)
- `Program.cs`: configuración de servicios (DbContext, Identity, JWT, CORS) y pipeline HTTP.
- `Infrastructure/Data/ApplicationDbContext.cs`: DbContext + configuración de Identity + filtros globales + seed de `TiposSeguro`.
- `Controllers/`: endpoints HTTP.

---
