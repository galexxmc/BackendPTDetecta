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

// --- AQUÍ ESTÁ LA MAGIA CENTRALIZADA ---
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // 1. Lógica robusta de Hora Perú (Mejor que AddHours(-5))
            var utcNow = DateTime.UtcNow;
            
            TimeZoneInfo zonaPeru;
            try { zonaPeru = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"); }
            catch { zonaPeru = TimeZoneInfo.FindSystemTimeZoneById("America/Lima"); }

            var fechaPeru = TimeZoneInfo.ConvertTimeFromUtc(utcNow, zonaPeru);

            // 2. Quitar milisegundos
            var fechaLimpia = new DateTime(
                fechaPeru.Year, fechaPeru.Month, fechaPeru.Day,
                fechaPeru.Hour, fechaPeru.Minute, fechaPeru.Second
            );

            foreach (var entry in ChangeTracker.Entries<EntidadAuditable>())
            {
                // A) NUEVO REGISTRO
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.FechaRegistro = fechaLimpia;
                    entry.Entity.EstadoRegistro = 1;
                }

                // B) MODIFICACIÓN (Aquí detectamos si es Edición normal o Eliminación)
                if (entry.State == EntityState.Modified)
                {
                    // Verificamos si se está dando de baja (Estado pasa a 0)
                    // entry.Property(...).CurrentValue nos da el valor que acabamos de asignar en el Repo
                    var estadoActual = entry.Property(x => x.EstadoRegistro).CurrentValue;

                    if (estadoActual == 0) 
                    {
                        // CASO: ELIMINACIÓN LÓGICA
                        // Solo ponemos la fecha de eliminación. 
                        // ¡NO tocamos FechaModificacion!
                        entry.Entity.FechaEliminacion = fechaLimpia;
                    }
                    else
                    {
                        // CASO: EDICIÓN NORMAL
                        // Aquí sí actualizamos la fecha de modificación
                        entry.Entity.FechaModificacion = fechaLimpia;
                    }
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

            modelBuilder.Entity<Paciente>().Property(p => p.Codigo).HasComputedColumnSql("'P' || LPAD(\"NU_ID_PACIENTE\"::TEXT, 5, '0')", stored: true);

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
                new TipoSeguro
                {
                    IdTipoSeguro = 1,
                    NombreSeguro = "SIS",
                    RucEmpresa = "20100000001",
                    TipoCobertura = "Integral",
                    CoPago = "0%",
                    FechaRegistro = DateTime.UtcNow,
                    EstadoRegistro = 1,
                    UsuarioRegistro = "SYSTEM"
                },
                new TipoSeguro
                {
                    IdTipoSeguro = 2,
                    NombreSeguro = "EsSalud",
                    RucEmpresa = "20500000002",
                    TipoCobertura = "Laboral",
                    CoPago = "0%",
                    FechaRegistro = DateTime.UtcNow,
                    EstadoRegistro = 1,
                    UsuarioRegistro = "SYSTEM"
                },
                new TipoSeguro
                {
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