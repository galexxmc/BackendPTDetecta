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
            entity.FechaEliminacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}