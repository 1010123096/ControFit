using ControlFit.Application.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ControlFit.Infrastructure.Repository
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerarToken(int administradorId, string correo, int? gimnasioId = null, string? nombreGimnasio = null)
        {
            var esSuperAdmin = !gimnasioId.HasValue;
            var role = esSuperAdmin ? "Super Admin" : "Admin Gimnasio";

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, administradorId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, correo),
                new Claim("role", role),
                new Claim("GimnasioId", (gimnasioId ?? 0).ToString())
            };

            if (!string.IsNullOrEmpty(nombreGimnasio))
                claims.Add(new Claim("nombreGimnasio", nombreGimnasio));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerarRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }
    }
}
