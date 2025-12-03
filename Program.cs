using BackendPTDetecta.Infrastructure.Data;
using BackendPTDetecta.Application.Interfaces;
using BackendPTDetecta.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
// --- NUEVOS IMPORTS PARA SEGURIDAD ---
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackendPTDetecta.Infrastructure.Services;
using BackendPTDetecta.Domain.Entities; // Para IdentityService


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Swagger (Recomendado tenerlo siempre)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. CORS
builder.Services.AddCors(o => o.AddPolicy("AllowReact", p => 
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// 2. Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// ==========================================
// 3. CONFIGURAR IDENTITY (USUARIOS) - ¡NUEVO!
// ==========================================
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>     
{
    // Relajamos las reglas de password para la prueba técnica
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ==========================================
// 4. CONFIGURAR JWT (TOKENS) - ¡NUEVO!
// ==========================================
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

// ==========================================
// 5. INYECCIÓN DE DEPENDENCIAS
// ==========================================
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IAuthService, IdentityService>(); // <--- CONECTAMOS EL SERVICIO DE AUTH

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReact");

// ==========================================
// 6. ACTIVAR MIDDLEWARE DE SEGURIDAD
// ==========================================
app.UseAuthentication(); // <--- ¡CRÍTICO! Primero verificamos "quién eres"
app.UseAuthorization();  // <--- Luego verificamos "qué puedes hacer"

app.MapControllers();

app.Run();