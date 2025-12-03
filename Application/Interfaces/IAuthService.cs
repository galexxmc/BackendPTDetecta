using BackendPTDetecta.Application.DTOs.Auth;

namespace BackendPTDetecta.Application.Interfaces
{
    public interface IAuthService
    {
        // Login: Devuelve el Token JWT si es exitoso
        Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request);

        // Registro: Devuelve el Token (login automático) o un booleano de éxito
        Task<AuthResponseDTO> RegistrarAsync(RegisterRequestDTO request);

        // Recuperación de Contraseña (Flujo Senior)
        Task<string> ForgotPasswordAsync(string email); 
        Task<bool> ResetPasswordAsync(ResetPasswordDTO request);
    }
}