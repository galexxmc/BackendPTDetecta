namespace BackendPTDetecta.Application.DTOs.Auth
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty; // El JWT (La llave de acceso)
        public string Email { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty; // Ej: "Admin", "Medico"
        public DateTime Expiracion { get; set; } // Para saber cuándo caduca el token
    }
}