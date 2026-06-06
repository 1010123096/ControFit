using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
  //  [Authorize]
    public class AutenticacionController : ControllerBase
    {
        private readonly RegistrarAdministrador _registrar;
        private readonly LoginAdministrador _login;
        public AutenticacionController(RegistrarAdministrador registrar, LoginAdministrador login)
        {
            _registrar = registrar;
            _login = login;
        }

      
        [HttpPost("registro")]
    public async Task<IActionResult> Registrar([FromBody] RegistroAdministradorDTO dto)
        {
            await _registrar.EjecutarAsyncRegistrar(dto);
            return Ok(new { mensaje = "Administrador registrado" });
        }
        [AllowAnonymous]
        [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginAdministradorDTO dto)
        {
            var id = await _login.EjecutarAsyncLogin(dto);
            if (id == null)
                return Unauthorized(new { mensaje = "Credenciales inválidas" });

            return Ok(new { mensaje = "Login exitoso", token = id });
        }
    }

}
