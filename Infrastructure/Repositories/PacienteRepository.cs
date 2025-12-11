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

        // 1. LECTURA (Sin cambios, esto estaba perfecto)
        public async Task<IEnumerable<Paciente>> ObtenerTodosConDetalleAsync()
        {
            return await _context.Pacientes
                .Include(p => p.TipoSeguro)      
                .Include(p => p.HistorialClinico) 
                .Where(p => p.EstadoRegistro == 1) 
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

        // 2. CREACIÓN (LIMPIEZA MAYOR)
        public async Task<Paciente> CrearAsync(Paciente paciente)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // PASO A: Guardar Paciente
                // NOTA: Ya no seteamos FechaRegistro ni UsuarioRegistro aquí.
                // El Interceptor lo hará al detectar EntityState.Added.
                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync(); 

                // PASO B: Generar Historial
                var historial = new HistorialClinico
                {
                    IdPaciente = paciente.IdPaciente,
                    CodigoHistoria = $"HC-{DateTime.Now.Year}-{paciente.IdPaciente:D4}",
                    FechaApertura = DateTime.Now, // Fecha de negocio (está bien dejarla si es explícita)
                    EstadoPacienteActual = "Ingresado",
                    GrupoSanguineo = "Pendiente",
                    AlergiasPrincipales = "Ninguna reportada",
                    EnfermedadesCronicas = "Ninguna reportada"
                    // ELIMINADO: UsuarioRegistro. El interceptor también llenará esto en el Historial.
                };

                _context.HistorialesClinicos.Add(historial);
                await _context.SaveChangesAsync(); // <-- El Interceptor vuelve a actuar aquí para el Historial

                await transaction.CommitAsync();
                return paciente;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; 
            }
        }

        // 3. ACTUALIZACIÓN (AUTOMÁTICA)
        public async Task<bool> ActualizarAsync(Paciente paciente)
        {
            // Al hacer Update, el estado pasa a Modified.
            // El Interceptor actualizará automáticamente FechaModificacion y UsuarioModificacion.
            _context.Pacientes.Update(paciente);
            var filasAfectadas = await _context.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        // 4. ELIMINACIÓN LÓGICA (AQUÍ ESTABA TU PROBLEMA DE "AdminWeb")
        // Fíjate que ya no necesito pedir el parámetro 'usuario' en el método
        public async Task<bool> EliminarLogicoAsync(int id, string motivo) 
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return false;

            // SOLO CAMBIAMOS EL ESTADO Y EL MOTIVO
            paciente.EstadoRegistro = 0; 
            paciente.MotivoEliminacion = motivo;

            // ELIMINADO: paciente.UsuarioEliminacion = ...
            // ELIMINADO: paciente.FechaEliminacion = ...
            
            // MAGIA: Al llamar a SaveChanges, el Interceptor ve que EstadoRegistro cambió a 0
            // y él mismo pone "gmonje" y la fecha actual.
            await _context.SaveChangesAsync();
            
            return true;
        }

        // 5. RECUPERACIÓN (Habilitar)
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
            
            // ELIMINADO: Auditoría manual. 
            // Al cambiar el estado, cuenta como una Modificación. 
            // El interceptor pondrá FechaModificacion y UsuarioModificacion (gmonje) automáticamente.

            await _context.SaveChangesAsync();
            return true;
        }

        // 6. VALIDACIONES EXTRAS
        public async Task<bool> ExisteDniAsync(string dni)
        {
            return await _context.Pacientes
                .AnyAsync(p => p.Dni == dni && p.EstadoRegistro == 1);
        }

        // Método auxiliar para la recuperación (sin cambios)
        public async Task<Paciente?> BuscarEliminadoPorDniAsync(string dni)
        {
            return await _context.Pacientes
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Dni == dni && p.EstadoRegistro == 0);
        }
    }
}