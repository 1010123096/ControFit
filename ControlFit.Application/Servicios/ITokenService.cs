namespace ControlFit.Application.Repository
{
    public interface ITokenService
    {
        string GenerarToken(int administradorId, string correo, int? gimnasioId = null, string? nombreGimnasio = null);
        string GenerarRefreshToken();
    }
}
