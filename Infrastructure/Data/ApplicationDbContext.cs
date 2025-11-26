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
        public DbSet<HistoriaClinica> HistoriasClinicas { get; set; }

        // Fix para .NET 9
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
            modelBuilder.Entity<Paciente>().HasQueryFilter(x => x.EstadoRegistro == 1);
            modelBuilder.Entity<TipoSeguro>().HasQueryFilter(x => x.EstadoRegistro == 1);
            
            // Datos Semilla
            modelBuilder.Entity<TipoSeguro>().HasData(
                new TipoSeguro { IdTipoSeguro = 1, Nombre = "SIS", FechaRegistro = DateTime.UtcNow, EstadoRegistro = 1, UsuarioRegistro = "SYSTEM" },
                new TipoSeguro { IdTipoSeguro = 2, Nombre = "EsSalud", FechaRegistro = DateTime.UtcNow, EstadoRegistro = 1, UsuarioRegistro = "SYSTEM" },
                new TipoSeguro { IdTipoSeguro = 3, Nombre = "Privado", FechaRegistro = DateTime.UtcNow, EstadoRegistro = 1, UsuarioRegistro = "SYSTEM" }
            );
        }
    }
}