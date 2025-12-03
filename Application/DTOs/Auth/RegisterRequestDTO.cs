using System.ComponentModel.DataAnnotations;

namespace BackendPTDetecta.Application.DTOs.Auth
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;

        // Opcional: Para asignar roles desde el registro (Admin, Medico, Recepcion)
        // Por defecto será "User" o lo que definamos en la lógica
        public string Rol { get; set; } = "User"; 
    }
}