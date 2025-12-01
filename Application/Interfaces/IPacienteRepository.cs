using BackendPTDetecta.Domain.Entities;

namespace BackendPTDetecta.Application.Interfaces
{
    public interface IPacienteRepository
    {
        Task<List<Paciente>> ObtenerTodosAsync();
        Task<Paciente?> ObtenerPorIdAsync(int id);
        Task<Paciente> CrearAsync(Paciente paciente);
        Task<bool> ActualizarAsync(Paciente paciente);
        Task<bool> EliminarLogicoAsync(int id, string usuario, string motivo);
        Task<Paciente?> BuscarEliminadoPorDniAsync(string dni);
        Task<bool> HabilitarPacienteAsync(int id);
    }
}