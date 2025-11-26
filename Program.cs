using BackendPTDetecta.Infrastructure.Data;
using BackendPTDetecta.Application.Interfaces;
using BackendPTDetecta.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// 1. CORS (Permitir React)
builder.Services.AddCors(o => o.AddPolicy("AllowReact", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// 2. Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. INYECCIÓN DE DEPENDENCIAS (La Magia de Onion)
// "Cuando alguien pida IPacienteRepository, entrégale PacienteRepository"
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

app.Run();