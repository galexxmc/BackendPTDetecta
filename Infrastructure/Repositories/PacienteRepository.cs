using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Domain.Entities;
using BackendPTDetecta.Infrastructure.Data;
using BackendPTDetecta.Application.Interfaces;

namespace BackendPTDetecta.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. LECTURA: Traer datos con relaciones (Eager Loading)
        public async Task<IEnumerable<Paciente>> ObtenerTodosConDetalleAsync()
        {
            return await _context.Pacientes
                .Include(p => p.TipoSeguro)      
                .Include(p => p.HistorialClinico) // JOIN con Historial
                .Where(p => p.EstadoRegistro == 1) // Solo activos (Soft Delete)
                .OrderByDescending(p => p.FechaRegistro)
                .ToListAsync();
        }

        public async Task<Paciente?> ObtenerPorIdConDetalleAsync(int id)
        {
            return await _context.Pacientes
                .Include(p => p.TipoSeguro)
                .Include(p => p.HistorialClinico)
                .FirstOrDefaultAsync(p => p.IdPaciente == id);
        }

        // 2. ESCRITURA TRANSACCIONAL (ACID)
        // Aquí ocurre la magia: Creamos Paciente + Historial en una sola operación atómica.
        public async Task<Paciente> CrearAsync(Paciente paciente)
        {
            // Iniciamos una transacción de base de datos manual
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // PASO A: Guardar el Paciente
                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync(); // Al guardar, SQL Server genera el IdPaciente

                // PASO B: Generar el Historial Clínico automáticamente
                // Usamos el ID recién generado del paciente
                var historial = new HistorialClinico
                {
                    IdPaciente = paciente.IdPaciente,
                    CodigoHistoria = $"HC-{DateTime.Now.Year}-{paciente.IdPaciente:D4}", // Ej: HC-2025-0045
                    FechaApertura = DateTime.UtcNow,
                    UsuarioRegistro = paciente.UsuarioRegistro,
                    EstadoPacienteActual = "Ingresado",
                    
                    // Valores por defecto para evitar nulos en BD
                    GrupoSanguineo = "Pendiente",
                    AlergiasPrincipales = "Ninguna reportada",
                    EnfermedadesCronicas = "Ninguna reportada"
                };

                _context.HistorialesClinicos.Add(historial);
                await _context.SaveChangesAsync();

                // PASO C: Si todo salió bien, confirmamos los cambios en la BD
                await transaction.CommitAsync();

                return paciente;
            }
            catch (Exception)
            {
                // PASO D: Si algo falló (ej: se cayó el internet a medio camino), 
                // deshacemos TODO. No se crea el paciente "huérfano" sin historial.
                await transaction.RollbackAsync();
                throw; 
            }
        }

        // 3. ACTUALIZACIÓN
        public async Task<bool> ActualizarAsync(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            var filasAfectadas = await _context.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        // 4. ELIMINACIÓN LÓGICA (Soft Delete)
        public async Task<bool> EliminarLogicoAsync(int id, string usuario, string motivo)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return false;

            // No borramos el registro (Remove), solo cambiamos su estado
            paciente.EstadoRegistro = 0; 
            paciente.UsuarioEliminacion = usuario;
            paciente.MotivoEliminacion = motivo;
            paciente.FechaEliminacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        // 5. RECUPERACIÓN (Habilitar)
        public async Task<Paciente?> BuscarEliminadoPorDniAsync(string dni)
        {
            // Usamos IgnoreQueryFilters() por si en el futuro configuras filtros globales en el DbContext
            return await _context.Pacientes
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Dni == dni && p.EstadoRegistro == 0);
        }

        public async Task<bool> HabilitarPacienteAsync(int id)
        {
            var paciente = await _context.Pacientes
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdPaciente == id);

            if (paciente == null) return false;

            // Restauramos el paciente
            paciente.EstadoRegistro = 1;
            paciente.UsuarioEliminacion = null;
            paciente.MotivoEliminacion = null;
            paciente.FechaEliminacion = null;
            
            // Auditoría de restauración
            paciente.UsuarioModificacion = "Sistema Recuperación";
            paciente.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        // 6. VALIDACIONES EXTRAS
        public async Task<bool> ExisteDniAsync(string dni)
        {
            return await _context.Pacientes
                .AnyAsync(p => p.Dni == dni && p.EstadoRegistro == 1);
        }
    }
}