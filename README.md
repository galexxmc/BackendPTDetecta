# 🏥 PT Detecta - API Backend

![.NET](https://img.shields.io/badge/.NET%209.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)

API RESTful robusta y escalable para la gestión de pacientes e historiales clínicos.  
Desarrollada con **.NET 9** siguiendo principios de **Clean Architecture**.

🔗 **URL Producción:**  
https://backendptdetecta.onrender.com

---

## 🛠️ Configuración y Ejecución Local

Sigue estos pasos para levantar la API en tu entorno local.

### 📌 Prerrequisitos

Asegúrate de tener instalado:

- **.NET SDK 9.0**
- **PostgreSQL**

---

## 📥 1. Clonar el Repositorio

```bash
git clone https://github.com/tu-usuario/BackendPTDetecta.git
cd BackendPTDetecta
```

---

## 🗄️ 2. Configurar la Base de Datos

Crea una base de datos en PostgreSQL llamada:

```text
DetectaDB
```

Luego, abre el archivo `appsettings.json` y actualiza la cadena de conexión:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=DetectaDB;Username=postgres;Password=tu_password"
}
```

---

## 🧩 3. Aplicar Migraciones

Ejecuta las migraciones de Entity Framework Core:

```bash
dotnet ef database update
```

---

## ▶️ 4. Ejecutar la Aplicación (API)

Inicia el servidor de desarrollo:

```bash
dotnet watch run
```

ℹ️ La API estará disponible en:

- `http://localhost:5036` *(si está configurado ese puerto)*
- o en los puertos predeterminados `5000 / 5001`

Revisa el archivo `launchSettings.json` si tienes dudas.

📄 **Swagger:**  
http://localhost:5036/swagger

---

## 🏗️ Arquitectura

El proyecto sigue los principios de **Clean Architecture (Monolito Modular)**:

- **Domain** → Entidades y lógica de negocio
- **Application** → Casos de uso e interfaces
- **Infrastructure** → EF Core, repositorios e Identity
- **API** → Controladores REST y configuración

---

## 👨‍💻 Autor

Desarrollador: **Gherson Alexis**
