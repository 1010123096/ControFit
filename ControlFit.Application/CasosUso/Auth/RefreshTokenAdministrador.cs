using ControlFit.Application.DTO;
using ControlFit.Application.Repository;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using System.Security.Cryptography;
using System.Text;

namespace ControlFit.Application.CasosUso.Auth
{
    public class RefreshTokenAdministrador
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAdministradorRepository _adminRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenAdministrador(
            IRefreshTokenRepository refreshTokenRepository,
            IAdministradorRepository adminRepository,
            ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _adminRepository = adminRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthTokenResponseDTO?> EjecutarAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken)) return null;

            var hash = HashToken(refreshToken);
            var stored = await _refreshTokenRepository.ObtenerPorHashAsync(hash);
            if (stored == null || !stored.IsActive) return null;

            var admin = await _adminRepository.ObtenerPorIdAsync(stored.AdministradorId);
            if (admin == null) return null;

            var nombreGimnasio = admin.Gimnasio?.Nombre;
            var accessToken = _tokenService.GenerarToken(admin.Id, admin.Correo, admin.GimnasioId, nombreGimnasio);
            var newRefresh = _tokenService.GenerarRefreshToken();
            var newHash = HashToken(newRefresh);

            await _refreshTokenRepository.RevocarAsync(stored, newHash);
            await _refreshTokenRepository.CrearAsync(new RefreshToken
            {
                AdministradorId = admin.Id,
                TokenHash = newHash,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            return new AuthTokenResponseDTO
            {
                Token = accessToken,
                RefreshToken = newRefresh,
                ExpiresAt = DateTime.UtcNow.AddHours(6)
            };
        }

        internal static string HashToken(string token)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
            return Convert.ToHexString(bytes);
        }
    }
}
