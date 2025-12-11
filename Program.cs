using BackendPTDetecta.Infrastructure.Data;
using BackendPTDetecta.Application.Interfaces;
using BackendPTDetecta.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackendPTDetecta.Infrastructure.Services;
using BackendPTDetecta.Domain.Entities;
// Asegúrate de tener este using para el Interceptor:
using BackendPTDetecta.Infrastructure.Persistence.Interceptors; 

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURACIÓN DE POSTGRES (Fechas)
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// 2. CONTROLLERS & SWAGGER
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. CORS
builder.Services.AddCors(o => o.AddPolicy("AllowReact", p => 
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// ---------------------------------------------------------
// BLOQUE DE AUDITORÍA Y SERVICIOS (AQUÍ ESTÁ LA MAGIA)
// ---------------------------------------------------------

// A. Permitir leer el usuario logueado desde el Token JWT
builder.Services.AddHttpContextAccessor(); // <--- NUEVO: VITAL

// B. Registrar tus servicios de infraestructura
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>(); // <--- NUEVO
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

// C. Registrar el Interceptor
builder.Services.AddScoped<AuditoriaInterceptor>(); // <--- NUEVO

// D. Configurar DB Context + Interceptor
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    // Resolvemos el interceptor desde el contenedor de inyección de dependencias
    var interceptor = sp.GetRequiredService<AuditoriaInterceptor>();

    options.UseNpgsql(connectionString)
           .AddInterceptors(interceptor); // <--- NUEVO: Aquí conectamos el cerebro de la auditoría
});

// ---------------------------------------------------------

// 4. IDENTITY (Usuarios y Roles)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>     
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 5. JWT (Autenticación)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

var app = builder.Build();

// 6. PIPELINE HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReact");

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapControllers();

app.Run();