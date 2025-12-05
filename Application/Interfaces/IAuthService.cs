using BackendPTDetecta.Application.DTOs.Auth;

namespace BackendPTDetecta.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request);
        Task<AuthResponseDTO> RegistrarAsync(RegisterRequestDTO request);
        Task<string> ForgotPasswordAsync(string email); 
        Task<bool> ResetPasswordAsync(ResetPasswordDTO request);
    }
}