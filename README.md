# 🏥 PT Detecta – Backend API

![.NET](https://img.shields.io/badge/.NET%209.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean%20Onion-success?style=for-the-badge)

## 📋 Descripción

**PT Detecta Backend** es el núcleo del sistema de detección temprana de riesgos de aprendizaje.  
Esta **API RESTful** centraliza la lógica de negocio, la seguridad y el acceso a datos, proporcionando servicios **seguros, escalables y mantenibles** para las aplicaciones cliente.

El sistema está desarrollado sobre **.NET 9**, priorizando buenas prácticas de ingeniería de software y un diseño desacoplado.

---

## 🏗️ Arquitectura y Tecnologías

El proyecto sigue el patrón de **Clean Architecture (Onion Architecture)**, desacoplando la lógica de negocio de la infraestructura externa y facilitando la evolución del sistema.

### 🧰 Stack Tecnológico

- **Core:** .NET 9 (C#)
- **Base de Datos:** PostgreSQL
- **ORM:** Entity Framework Core (Code First)
- **Documentación:** Swagger / OpenAPI

### 🧱 Estructura de Capas

1. **Domain**  
   Entidades y reglas de negocio puras (sin dependencias externas).

2. **Application**  
   Casos de uso, DTOs e interfaces.

3. **Infrastructure**  
   Implementaciones de persistencia, repositorios y servicios externos.

4. **API**  
   Controladores REST, configuración y middlewares.

---

## ⚙️ Guía de Ejecución Local

Sigue estos pasos para ejecutar la API en tu entorno local.

### 1️⃣ Prerrequisitos

- **.NET SDK 9.0**
- **PostgreSQL** en ejecución

---

### 2️⃣ Clonar el Repositorio

```bash
git clone https://github.com/tu-usuario/BackendPTDetecta.git
cd BackendPTDetecta
```

---

### 3️⃣ Configuración de Base de Datos

Ubica el archivo `appsettings.json` en el proyecto **API** y configura la cadena de conexión:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=DetectaDB;Username=postgres;Password=tu_password"
}
```

---

### 4️⃣ Generar la Base de Datos

Ejecuta las migraciones para crear automáticamente la estructura:

```bash
dotnet ef database update
```

---

### 5️⃣ Iniciar la API

```bash
dotnet watch run
```

📍 **API:** http://localhost:5036  
📄 **Swagger:** http://localhost:5036/swagger

---

## 👨‍💻 Autor

Desarrollado por **Gherson Alexis**
