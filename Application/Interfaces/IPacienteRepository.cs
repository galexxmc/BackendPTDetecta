using BackendPTDetecta.Domain.Entities;

namespace BackendPTDetecta.Application.Interfaces
{
    public interface IPacienteRepository
    {
        // Obtener el Detalle del Paciente
        Task<IEnumerable<Paciente>> ObtenerTodosConDetalleAsync();
        Task<Paciente?> ObtenerPorIdConDetalleAsync(int id);

        // Crear/Actualizar Paciente
        Task<Paciente> CrearAsync(Paciente paciente);
        Task<bool> ActualizarAsync(Paciente paciente);

        // Eliminar Paciente

        // Recuperar Paciente
        Task<Paciente?> BuscarEliminadoPorDniAsync(string dni);
        Task<bool> HabilitarPacienteAsync(int id);

        // Validación de Dominio
        Task<bool> ExisteDniAsync(string dni);
        
        // Eliminar Paciente
        Task<bool> EliminarLogicoAsync(int id, string motivo);

    }
}