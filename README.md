# 🏥 PT Detecta - API Backend

![.NET](https://img.shields.io/badge/.NET%209.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Render](https://img.shields.io/badge/Render-46E3B7?style=for-the-badge&logo=render&logoColor=white)

API RESTful robusta y escalable para la gestión de pacientes e historiales clínicos. Desarrollada con **.NET 9** siguiendo los principios de **Clean Architecture (Onion)**.

🔗 **URL Producción:** `https://backendptdetecta.onrender.com`

---

## 🏗️ Arquitectura
El sistema sigue un diseño de **Monolito Modular** dividido en capas:
* **Domain:** Entidades y Lógica de Negocio (Sin dependencias).
* **Application:** Casos de uso, DTOs e Interfaces.
* **Infrastructure:** EF Core, Repositorios, Identity y Servicios Externos.
* **API:** Controladores REST y configuración de dependencias.

---

## ⚙️ Configuración Local

### Prerrequisitos
* [.NET SDK 9.0](https://dotnet.microsoft.com/download)
* PostgreSQL

### 1. Configurar Base de Datos
Actualiza el archivo `appsettings.json` con tu cadena de conexión local:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=DetectaDB;Username=postgres;Password=tu_password"
}