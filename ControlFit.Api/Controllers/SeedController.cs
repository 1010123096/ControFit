using ControlFit.Application.DTO;
using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.CasosUso.CRUDGimnasio;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/seed")]
    public class SeedController : ControllerBase
    {
        private readonly RegistrarAdministrador _registrar;
        private readonly GimnasioService _gimnasioService;
        private const string ClavePorDefecto = "123456";

        public SeedController(RegistrarAdministrador registrar, GimnasioService gimnasioService)
        {
            _registrar = registrar;
            _gimnasioService = gimnasioService;
        }

        [HttpPost]
        public async Task<IActionResult> Seed()
        {
            try
            {
                var gymDto = new GimnasioCrearDTO("FitZone Gym", "Av. Principal 123, Lima", "999-888-777")
                {
                    Estado = true
                };
                var gym = await _gimnasioService.CrearGimnasio(gymDto);

                var superAdminDto = new RegistroAdministradorDTO
                {
                    nombreCompleto = "Super Admin",
                    correo = "admin@controlfit.com",
                    contrasena = ClavePorDefecto,
                    GimnasioId = null
                };
                await _registrar.EjecutarAsyncRegistrar(superAdminDto);

                var gymAdminDto = new RegistroAdministradorDTO
                {
                    nombreCompleto = "Admin FitZone",
                    correo = "gym@fitzone.com",
                    contrasena = ClavePorDefecto,
                    GimnasioId = gym.Id
                };
                await _registrar.EjecutarAsyncRegistrar(gymAdminDto);

                return Ok(new
                {
                    message = "Datos de prueba creados exitosamente",
                    superAdmin = new { correo = "admin@controlfit.com", contrasena = ClavePorDefecto, tipo = "Super Admin", ruta = "/super-admin/dashboard" },
                    gymAdmin = new { correo = "gym@fitzone.com", contrasena = ClavePorDefecto, tipo = "Admin Gimnasio", ruta = "/gym-admin/dashboard", gimnasio = "FitZone Gym" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
