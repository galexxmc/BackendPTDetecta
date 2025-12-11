using BackendPTDetecta.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BackendPTDetecta.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string ObtenerCodigoUsuario()
        {
            // Busca el claim "CodigoUsuario" dentro del Token JWT
            var codigo = _httpContextAccessor.HttpContext?.User?.FindFirst("CodigoUsuario")?.Value;
            return !string.IsNullOrEmpty(codigo) ? codigo : "sistema";
        }
    }
}