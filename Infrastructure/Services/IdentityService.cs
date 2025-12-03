using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendPTDetecta.Application.DTOs.Auth;
using BackendPTDetecta.Application.Interfaces;
using BackendPTDetecta.Domain.Entities; // <--- Importante para ApplicationUser
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackendPTDetecta.Infrastructure.Services
{
    public class IdentityService : IAuthService
    {
        // 1. CAMBIO AQUÍ: Usamos ApplicationUser en lugar de IdentityUser
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        // 2. CAMBIO EN CONSTRUCTOR: Inyectamos los managers tipados correctamente
        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            // Ahora 'user' es de tipo ApplicationUser automáticamente
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null) throw new Exception("Usuario o contraseña incorrectos.");

            // CheckPasswordSignInAsync ahora recibe el tipo correcto
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded) throw new Exception("Usuario o contraseña incorrectos.");

            // Llamamos al método síncrono (sin await)
            return GenerarTokenRespuesta(user);
        }

        public async Task<AuthResponseDTO> RegistrarAsync(RegisterRequestDTO request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Nombres = request.Nombre,     // Guardamos Nombres
                Apellidos = request.Apellido, // Guardamos Apellidos
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Error al registrar: {errors}");
            }

            return GenerarTokenRespuesta(user);
        }

        // 3. CAMBIO AQUÍ: Método Síncrono (Quitamos async Task) para eliminar el Warning CS1998
        private AuthResponseDTO GenerarTokenRespuesta(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // Concatenamos nombre completo real
                new Claim("nombre_completo", $"{user.Nombres} {user.Apellidos}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponseDTO
            {
                Token = tokenHandler.WriteToken(token),
                Email = user.Email!,
                NombreCompleto = $"{user.Nombres} {user.Apellidos}",
                Rol = "User",
                Expiracion = tokenDescriptor.Expires.Value
            };
        }
        // 1. SOLICITAR RECUPERACIÓN
        public async Task<string> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            // SEGURIDAD: Si el usuario no existe, NO debemos decir "No existe".
            // Decimos "Si el correo existe, se envió un mensaje" para evitar enumeración de usuarios.
            if (user == null) return "Si el correo es válido, se ha enviado un enlace de recuperación.";

            // Generamos el token único de Identity
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // EN PRODUCCIÓN: Aquí enviarías el email.
            // await _emailService.SendEmailAsync(email, "Recuperar Clave", $"Tu token es: {token}");

            // PARA TU PRUEBA TÉCNICA: Devolvemos el token para que lo copies y pegues
            return token;
        }

        // 2. RESTABLECER CONTRASEÑA
        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return false; // O lanzar excepción genérica

            // Identity valida el token y la nueva contraseña automáticamente
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Error al restaurar: {errors}");
            }

            return true;
        }
    }
}