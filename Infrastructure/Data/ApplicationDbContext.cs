using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BackendPTDetecta.Domain.Entities;

namespace BackendPTDetecta.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<TipoSeguro> TiposSeguro { get; set; }
        // Cambié el nombre del DbSet para que coincida con tu clase nueva
        public DbSet<HistorialClinico> HistorialesClinicos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntidadAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.FechaRegistro = DateTime.UtcNow;
                    entry.Entity.EstadoRegistro = 1;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.FechaModificacion = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Filtros Globales (No traer eliminados)
            modelBuilder.Entity<Paciente>().HasQueryFilter(x => x.EstadoRegistro == 1);
            modelBuilder.Entity<TipoSeguro>().HasQueryFilter(x => x.EstadoRegistro == 1);
            modelBuilder.Entity<HistorialClinico>().HasQueryFilter(x => x.EstadoRegistro == 1);

            // CONFIGURACIÓN AVANZADA
            
            // DNI Único (Regla de negocio estricta)
            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Dni)
                .IsUnique();

            // Relación 1 a 1: Un Paciente tiene UN Historial
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.HistorialClinico)
                .WithOne(h => h.Paciente)
                .HasForeignKey<HistorialClinico>(h => h.IdPaciente);

            // DATOS SEMILLA (Con los nuevos campos RUC y Cobertura)
            modelBuilder.Entity<TipoSeguro>().HasData(
                new TipoSeguro { 
                    IdTipoSeguro = 1, 
                    NombreSeguro = "SIS", 
                    RucEmpresa = "20100000001", 
                    TipoCobertura = "Integral", 
                    CoPago = "0%", 
                    FechaRegistro = DateTime.UtcNow, 
                    EstadoRegistro = 1, 
                    UsuarioRegistro = "SYSTEM" 
                },
                new TipoSeguro { 
                    IdTipoSeguro = 2, 
                    NombreSeguro = "EsSalud", 
                    RucEmpresa = "20500000002", 
                    TipoCobertura = "Laboral", 
                    CoPago = "0%", 
                    FechaRegistro = DateTime.UtcNow, 
                    EstadoRegistro = 1, 
                    UsuarioRegistro = "SYSTEM" 
                },
                new TipoSeguro { 
                    IdTipoSeguro = 3, 
                    NombreSeguro = "EPS Pacifico", 
                    RucEmpresa = "20600000003", 
                    TipoCobertura = "Privada", 
                    CoPago = "20%", 
                    FechaRegistro = DateTime.UtcNow, 
                    EstadoRegistro = 1, 
                    UsuarioRegistro = "SYSTEM" 
                }
            );
        }
    }
}