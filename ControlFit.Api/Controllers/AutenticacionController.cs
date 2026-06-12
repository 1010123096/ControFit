using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AutenticacionController : ControllerBase
    {
        private readonly RegistrarAdministrador _registrar;
        private readonly LoginAdministrador _login;
        private readonly RefreshTokenAdministrador _refresh;
        private readonly LogoutAdministrador _logout;
        private readonly IUserContextService _userContext;

        public AutenticacionController(
            RegistrarAdministrador registrar,
            LoginAdministrador login,
            RefreshTokenAdministrador refresh,
            LogoutAdministrador logout,
            IUserContextService userContext)
        {
            _registrar = registrar;
            _login = login;
            _refresh = refresh;
            _logout = logout;
            _userContext = userContext;
        }

        [Authorize(Policy = "AnyAdmin")]
        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] RegistroAdministradorDTO dto)
        {
            try
            {
                if (_userContext.EsSuperAdmin())
                {
                    await _registrar.EjecutarAsyncRegistrar(dto);
                }
                else if (_userContext.EsAdminGimnasio())
                {
                    if (dto.GimnasioId is null or 0)
                        return BadRequest(new { error = "Solo puede registrar administradores de su gimnasio." });

                    if (dto.GimnasioId != _userContext.GetGimnasioId())
                        return Unauthorized(new { error = "No puede registrar administradores en otro gimnasio." });

                    await _registrar.EjecutarAsyncRegistrar(dto);
                }
                else
                {
                    return Unauthorized(new { error = "No autorizado." });
                }

                return Ok(new { mensaje = "Administrador registrado" });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAdministradorDTO dto)
        {
            var tokens = await _login.EjecutarAsyncLogin(dto);
            if (tokens == null)
                return Unauthorized(new { mensaje = "Credenciales inválidas" });

            return Ok(new
            {
                mensaje = "Login exitoso",
                token = tokens.Token,
                refreshToken = tokens.RefreshToken,
                expiresAt = tokens.ExpiresAt
            });
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDTO dto)
        {
            var tokens = await _refresh.EjecutarAsync(dto.RefreshToken);
            if (tokens == null)
                return Unauthorized(new { mensaje = "Refresh token inválido o expirado" });

            return Ok(new
            {
                mensaje = "Token renovado",
                token = tokens.Token,
                refreshToken = tokens.RefreshToken,
                expiresAt = tokens.ExpiresAt
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDTO? dto)
        {
            await _logout.EjecutarAsync(dto?.RefreshToken);
            return Ok(new { mensaje = "Sesión cerrada" });
        }
    }
}
