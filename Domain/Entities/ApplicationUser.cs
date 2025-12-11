using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackendPTDetecta.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)] public string Nombres { get; set; } = string.Empty;
        [MaxLength(100)] public string Apellidos { get; set; } = string.Empty;
        [MaxLength(20)] public string CodigoUsuario { get; set; } = string.Empty;
    }
}