using ControlFit.Domain.Entidad;

namespace ControlFit.Domain.Interfaz_puertos_
{
    public interface IRefreshTokenRepository
    {
        Task CrearAsync(RefreshToken token);
        Task<RefreshToken?> ObtenerPorHashAsync(string tokenHash);
        Task RevocarAsync(RefreshToken token, string? replacedByHash = null);
        Task RevocarTodosDelAdministradorAsync(int administradorId);
    }
}
