using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.CasosUso.CRUDGimnasio;
using ControlFit.Application.CasosUso.Seed;
using ControlFit.Application.DTO;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/seed")]
    public class SeedController : ControllerBase
    {
        private readonly RegistrarAdministrador _registrar;
        private readonly GimnasioService _gimnasioService;
        private readonly SeedDemoDataService _demoData;
        private readonly IAdministradorRepository _adminRepo;
        private readonly IGimnasioRepository _gymRepo;
        private readonly IWebHostEnvironment _environment;
        private const string ClavePorDefecto = "123456";
        private const string NombreGimnasioSeed = "CossGym";

        public SeedController(
            RegistrarAdministrador registrar,
            GimnasioService gimnasioService,
            SeedDemoDataService demoData,
            IAdministradorRepository adminRepo,
            IGimnasioRepository gymRepo,
            IWebHostEnvironment environment)
        {
            _registrar = registrar;
            _gimnasioService = gimnasioService;
            _demoData = demoData;
            _adminRepo = adminRepo;
            _gymRepo = gymRepo;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Seed()
        {
            if (!_environment.IsDevelopment())
                return NotFound();

            try
            {
                var gym = await ObtenerOCrearGimnasioAsync();

                await EnsureAdminAsync(new RegistroAdministradorDTO
                {
                    nombreCompleto = "Super Admin",
                    correo = "admin@controlfit.com",
                    contrasena = ClavePorDefecto,
                    GimnasioId = null
                });

                await EnsureAdminAsync(new RegistroAdministradorDTO
                {
                    nombreCompleto = "Admin CossGym",
                    correo = "gym@cossgym.com",
                    contrasena = ClavePorDefecto,
                    GimnasioId = gym.Id
                });

                var demo = await _demoData.SeedAsync(gym.Id);

                return Ok(new
                {
                    message = "Datos de prueba listos",
                    superAdmin = new { correo = "admin@controlfit.com", contrasena = ClavePorDefecto, ruta = "/super-admin/dashboard" },
                    gymAdmin = new { correo = "gym@cossgym.com", contrasena = ClavePorDefecto, ruta = "/gym-admin/dashboard", gimnasio = NombreGimnasioSeed },
                    datos = new
                    {
                        demo.Miembros,
                        demo.Membresias,
                        demo.Asignaciones,
                        demo.Asistencias
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        private async Task<Gimnasio> ObtenerOCrearGimnasioAsync()
        {
            var gyms = await _gymRepo.ListarTodos();
            var existente = gyms.FirstOrDefault(g =>
                string.Equals(g.Nombre, NombreGimnasioSeed, StringComparison.OrdinalIgnoreCase));

            if (existente != null)
                return existente;

            var gymDto = new GimnasioCrearDTO(NombreGimnasioSeed, "Av. Principal 123, Lima", "999-888-777")
            {
                Estado = true
            };
            return await _gimnasioService.CrearGimnasio(gymDto);
        }

        private async Task EnsureAdminAsync(RegistroAdministradorDTO dto)
        {
            var existing = await _adminRepo.ObtenerPorCorreoAsync(dto.correo);
            if (existing == null)
            {
                await _registrar.EjecutarAsyncRegistrar(dto);
                return;
            }

            if (!existing.ValidarPassword(ClavePorDefecto))
            {
                existing.AsignarContrasena(ClavePorDefecto);
                await _adminRepo.ActualizarAsync(existing);
            }
        }
    }
}
