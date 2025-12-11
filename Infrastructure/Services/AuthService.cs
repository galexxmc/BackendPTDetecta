using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendPTDetecta.Application.DTOs.Auth;
using BackendPTDetecta.Application.Interfaces;
using BackendPTDetecta.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// ASP.NET Core Identity

namespace BackendPTDetecta.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // MÉTODO PARA GENERAR EL TOKEN JWT (ALGORITMO: HmacSha256)
        private AuthResponseDTO GenerarTokenRespuesta(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("nombre_completo", $"{user.Nombres} {user.Apellidos}"),
                new Claim("CodigoUsuario", user.CodigoUsuario ?? "sistema")
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

        // MÉTODO PARA EL LOGIN
        public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) throw new Exception("Usuario o contraseña incorrectos.");
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) throw new Exception("Usuario o contraseña incorrectos.");
            return GenerarTokenRespuesta(user);
        }

        // MÉTODO PARA EL REGISTRO (PBKDF2)
        public async Task<AuthResponseDTO> RegistrarAsync(RegisterRequestDTO request)
        {
            string codigo = GenerarCodigoBase(request.Nombre, request.Apellido);
            if (await _userManager.Users.AnyAsync(u => u.CodigoUsuario == codigo))
            {
                codigo += new Random().Next(10, 99).ToString();
            }
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Nombres = request.Nombre,
                Apellidos = request.Apellido,
                CodigoUsuario = codigo,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) throw new Exception("Error...");

            return GenerarTokenRespuesta(user);
        }

        // MÉTODO PARA RECUPERAR CONTRASEÑA
        public async Task<string> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return "Si el correo es válido, se ha enviado un enlace de recuperación.";
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        // MÉTODO PARA RESETEAR CONTRASEÑA
        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return false;

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Error al restaurar: {errors}");
            }

            return true;
        }

        // MÉTODO PRIVADO PARA GENERAR CÓDIGO (Helper)
        private string GenerarCodigoBase(string nombre, string apellido)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido)) return "user_gen";
            var inicial = nombre.Trim().Substring(0, 1).ToLower();
            var primerApellido = apellido.Trim().Split(' ')[0].ToLower();
            return $"{inicial}{primerApellido}";
        }
    }
}