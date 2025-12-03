using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BackendPTDetecta.Domain.Entities;

namespace BackendPTDetecta.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<TipoSeguro> TiposSeguro { get; set; }
        public DbSet<HistorialClinico> HistorialesClinicos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        // --- LÓGICA DE AUDITORÍA (Tu código original) ---
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var utcNow = DateTime.UtcNow;
            
            TimeZoneInfo zonaPeru;
            try { zonaPeru = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"); }
            catch { zonaPeru = TimeZoneInfo.FindSystemTimeZoneById("America/Lima"); }

            var fechaPeru = TimeZoneInfo.ConvertTimeFromUtc(utcNow, zonaPeru);
            var fechaLimpia = new DateTime(fechaPeru.Year, fechaPeru.Month, fechaPeru.Day, fechaPeru.Hour, fechaPeru.Minute, fechaPeru.Second);

            foreach (var entry in ChangeTracker.Entries<EntidadAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.FechaRegistro = fechaLimpia;
                    entry.Entity.EstadoRegistro = 1;
                }
                if (entry.State == EntityState.Modified)
                {
                    var estadoActual = entry.Property(x => x.EstadoRegistro).CurrentValue;
                    if (estadoActual == 0) entry.Entity.FechaEliminacion = fechaLimpia;
                    else entry.Entity.FechaModificacion = fechaLimpia;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. IMPORTANTE: Configuración Base de Identity
            base.OnModelCreating(modelBuilder);

            // ==============================================================================
            // 🛡️ PERSONALIZACIÓN TOTAL DE IDENTITY (Tablas y Columnas)
            // ==============================================================================

            // 1. TABLA USUARIOS (SEG_USUARIOS)
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("SEG_USUARIOS");
                b.Property(u => u.Id).HasColumnName("TX_ID_USUARIO");
                b.Property(u => u.UserName).HasColumnName("TX_USERNAME");
                b.Property(u => u.NormalizedUserName).HasColumnName("TX_USERNAME_NORM");
                b.Property(u => u.Email).HasColumnName("TX_EMAIL");
                b.Property(u => u.NormalizedEmail).HasColumnName("TX_EMAIL_NORM");
                b.Property(u => u.EmailConfirmed).HasColumnName("BL_EMAIL_CONFIRMADO");
                b.Property(u => u.PasswordHash).HasColumnName("TX_PASSWORD_HASH");
                b.Property(u => u.SecurityStamp).HasColumnName("TX_SECURITY_STAMP");
                b.Property(u => u.ConcurrencyStamp).HasColumnName("TX_CONCURRENCY_STAMP");
                b.Property(u => u.PhoneNumber).HasColumnName("TX_TELEFONO");
                b.Property(u => u.PhoneNumberConfirmed).HasColumnName("BL_TELEFONO_CONFIRMADO");
                b.Property(u => u.TwoFactorEnabled).HasColumnName("BL_2FA_ENABLED");
                b.Property(u => u.LockoutEnd).HasColumnName("FE_FIN_BLOQUEO");
                b.Property(u => u.LockoutEnabled).HasColumnName("BL_BLOQUEO_ENABLED");
                b.Property(u => u.AccessFailedCount).HasColumnName("NU_INTENTOS_FALLIDOS");
                b.Property(u => u.Nombres).HasColumnName("TX_NOMBRES");
                b.Property(u => u.Apellidos).HasColumnName("TX_APELLIDOS");
            });

            // 2. TABLA ROLES (SEG_ROLES)
            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.ToTable("SEG_ROLES");
                b.Property(r => r.Id).HasColumnName("TX_ID_ROL");
                b.Property(r => r.Name).HasColumnName("TX_NOMBRE_ROL");
                b.Property(r => r.NormalizedName).HasColumnName("TX_NOMBRE_ROL_NORM");
                b.Property(r => r.ConcurrencyStamp).HasColumnName("TX_CONCURRENCY_STAMP");
            });

            // 3. TABLA USUARIO_ROLES (SEG_USUARIO_ROLES) - Relación N a N
            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("SEG_USUARIO_ROLES");
                b.Property(ur => ur.UserId).HasColumnName("TX_ID_USUARIO");
                b.Property(ur => ur.RoleId).HasColumnName("TX_ID_ROL");
            });

            // 4. TABLA CLAIMS DE USUARIO (SEG_USUARIO_CLAIMS)
            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("SEG_USUARIO_CLAIMS");
                b.Property(uc => uc.Id).HasColumnName("NU_ID_CLAIM");
                b.Property(uc => uc.UserId).HasColumnName("TX_ID_USUARIO");
                b.Property(uc => uc.ClaimType).HasColumnName("TX_TIPO_CLAIM");
                b.Property(uc => uc.ClaimValue).HasColumnName("TX_VALOR_CLAIM");
            });

            // 5. TABLA CLAIMS DE ROL (SEG_ROL_CLAIMS)
            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("SEG_ROL_CLAIMS");
                b.Property(rc => rc.Id).HasColumnName("NU_ID_ROL_CLAIM");
                b.Property(rc => rc.RoleId).HasColumnName("TX_ID_ROL");
                b.Property(rc => rc.ClaimType).HasColumnName("TX_TIPO_CLAIM");
                b.Property(rc => rc.ClaimValue).HasColumnName("TX_VALOR_CLAIM");
            });

            // 6. TABLA LOGINS EXTERNOS (SEG_USUARIO_LOGINS) - Google, Facebook, etc.
            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("SEG_USUARIO_LOGINS");
                b.Property(ul => ul.LoginProvider).HasColumnName("TX_PROVEEDOR");
                b.Property(ul => ul.ProviderKey).HasColumnName("TX_LLAVE_PROVEEDOR");
                b.Property(ul => ul.ProviderDisplayName).HasColumnName("TX_NOMBRE_PROVEEDOR");
                b.Property(ul => ul.UserId).HasColumnName("TX_ID_USUARIO");
            });

            // 7. TABLA TOKENS DE USUARIO (SEG_USUARIO_TOKENS) - Recuperar pass, confirmar email
            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("SEG_USUARIO_TOKENS");
                b.Property(ut => ut.UserId).HasColumnName("TX_ID_USUARIO");
                b.Property(ut => ut.LoginProvider).HasColumnName("TX_PROVEEDOR");
                b.Property(ut => ut.Name).HasColumnName("TX_NOMBRE_TOKEN");
                b.Property(ut => ut.Value).HasColumnName("TX_VALOR_TOKEN");
            });

            // ==============================================================================
            // 🏥 TUS TABLAS DE NEGOCIO (PACIENTES, ETC.)
            // ==============================================================================

            modelBuilder.Entity<Paciente>().HasQueryFilter(x => x.EstadoRegistro == 1);
            modelBuilder.Entity<TipoSeguro>().HasQueryFilter(x => x.EstadoRegistro == 1);
            modelBuilder.Entity<HistorialClinico>().HasQueryFilter(x => x.EstadoRegistro == 1);

            modelBuilder.Entity<Paciente>().Property(p => p.Codigo)
                .HasComputedColumnSql("'P' || LPAD(\"NU_ID_PACIENTE\"::TEXT, 5, '0')", stored: true);

            modelBuilder.Entity<Paciente>().HasIndex(p => p.Dni).IsUnique();

            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.HistorialClinico)
                .WithOne(h => h.Paciente)
                .HasForeignKey<HistorialClinico>(h => h.IdPaciente);

            // TIPO_SEGUROS (Seed Data)
            modelBuilder.Entity<TipoSeguro>().HasData(
                new TipoSeguro { IdTipoSeguro = 1, NombreSeguro = "SIS", RucEmpresa = "20100000001", TipoCobertura = "Integral", CoPago = "0%", FechaRegistro = DateTime.UtcNow, EstadoRegistro = 1, UsuarioRegistro = "SYSTEM" },
                new TipoSeguro { IdTipoSeguro = 2, NombreSeguro = "EsSalud", RucEmpresa = "20500000002", TipoCobertura = "Laboral", CoPago = "0%", FechaRegistro = DateTime.UtcNow, EstadoRegistro = 1, UsuarioRegistro = "SYSTEM" },
                new TipoSeguro { IdTipoSeguro = 3, NombreSeguro = "EPS Pacifico", RucEmpresa = "20600000003", TipoCobertura = "Privada", CoPago = "20%", FechaRegistro = DateTime.UtcNow, EstadoRegistro = 1, UsuarioRegistro = "SYSTEM" }
            );
        }
    }
}