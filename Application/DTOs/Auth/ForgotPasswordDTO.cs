using System.ComponentModel.DataAnnotations;

namespace BackendPTDetecta.Application.DTOs.Auth
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}