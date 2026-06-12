using ControlFit.Application.DTO;
using ControlFit.Application.Repository;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using System.Security.Cryptography;

namespace ControlFit.Application.CasosUso.Auth
{
    public class LoginAdministrador
    {
        private readonly IAdministradorRepository _repo;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly AuditService _auditService;

        public LoginAdministrador(
            IAdministradorRepository repo,
            ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepository,
            AuditService auditService)
        {
            _repo = repo;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _auditService = auditService;
        }

        public async Task<AuthTokenResponseDTO?> EjecutarAsyncLogin(LoginAdministradorDTO dto)
        {
            var administrador = await _repo.ObtenerPorCorreoAsync(dto.correo);
            if (administrador == null || !administrador.ValidarPassword(dto.contrasena))
            {
                if (administrador != null)
                    await _auditService.RegistrarLoginAsync(administrador.Id, administrador.GimnasioId, false);
                return null;
            }

            var nombreGimnasio = administrador.Gimnasio?.Nombre;
            var accessToken = _tokenService.GenerarToken(
                administrador.Id,
                administrador.Correo,
                administrador.GimnasioId,
                nombreGimnasio);

            var refreshToken = _tokenService.GenerarRefreshToken();
            await _refreshTokenRepository.CrearAsync(new RefreshToken
            {
                AdministradorId = administrador.Id,
                TokenHash = RefreshTokenAdministrador.HashToken(refreshToken),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            await _auditService.RegistrarLoginAsync(administrador.Id, administrador.GimnasioId, true);

            return new AuthTokenResponseDTO
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(6)
            };
        }
    }
}
