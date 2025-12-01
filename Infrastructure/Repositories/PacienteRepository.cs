using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Application.Interfaces;
using BackendPTDetecta.Domain.Entities;
using BackendPTDetecta.Infrastructure.Data;

namespace BackendPTDetecta.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Paciente>> ObtenerTodosAsync()
        {
            return await _context.Pacientes
                .Include(p => p.TipoSeguro) // JOIN automático
                .ToListAsync();
        }

        public async Task<Paciente?> ObtenerPorIdAsync(int id)
        {
            return await _context.Pacientes
                .Include(p => p.TipoSeguro)
                .FirstOrDefaultAsync(p => p.IdPaciente == id);
        }

        public async Task<Paciente> CrearAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<bool> ActualizarAsync(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarLogicoAsync(int id, string usuario, string motivo)
        {
            var entity = await _context.Pacientes.FindAsync(id);
            if (entity == null) return false;

            entity.EstadoRegistro = 0;
            entity.UsuarioEliminacion = usuario;
            entity.MotivoEliminacion = motivo;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Paciente?> BuscarEliminadoPorDniAsync(string dni)
        {
            // IgnoreQueryFilters es OBLIGATORIO para poder ver los EstadoRegistro = 0
            return await _context.Pacientes
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Dni == dni && p.EstadoRegistro == 0);
        }

        public async Task<bool> HabilitarPacienteAsync(int id)
        {
            // Buscamos incluso entre los eliminados
            var paciente = await _context.Pacientes
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdPaciente == id);

            if (paciente == null) return false;

            // Reactivamos al paciente
            paciente.EstadoRegistro = 1;
            
            // Opcional: Actualizar fecha modificación para auditoría
            // paciente.UsuarioModificacion = "SistemaHabilitacion";

            await _context.SaveChangesAsync();
            return true;
        }       
    }
}