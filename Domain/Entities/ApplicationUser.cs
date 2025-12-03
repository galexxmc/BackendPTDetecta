using Microsoft.AspNetCore.Identity;

namespace BackendPTDetecta.Domain.Entities
{
    // Heredamos de IdentityUser para tener todo lo de Microsoft + lo nuestro
    public class ApplicationUser : IdentityUser
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        
        // Aquí podrías agregar DNI, FotoUrl, etc. en el futuro
    }
}