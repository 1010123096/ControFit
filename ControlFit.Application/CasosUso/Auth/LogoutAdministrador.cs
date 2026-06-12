using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Interfaz_puertos_;

namespace ControlFit.Application.CasosUso.Auth
{
    public class LogoutAdministrador
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserContextService _userContext;

        public LogoutAdministrador(IRefreshTokenRepository refreshTokenRepository, IUserContextService userContext)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userContext = userContext;
        }

        public async Task EjecutarAsync(string? refreshToken)
        {
            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                var hash = RefreshTokenAdministrador.HashToken(refreshToken);
                var stored = await _refreshTokenRepository.ObtenerPorHashAsync(hash);
                if (stored != null && stored.IsActive)
                    await _refreshTokenRepository.RevocarAsync(stored);
            }

            var adminId = _userContext.GetAdministradorId();
            if (adminId > 0)
                await _refreshTokenRepository.RevocarTodosDelAdministradorAsync(adminId);
        }
    }
}
