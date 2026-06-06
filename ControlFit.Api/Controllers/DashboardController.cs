using ControlFit.Application.CasosUso.Dashboard;
using ControlFit.Application.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;
        private readonly IUserContextService _userContext;

        public DashboardController(DashboardService dashboardService, IUserContextService userContext)
        {
            _dashboardService = dashboardService;
            _userContext = userContext;
        }

        [HttpGet("gym-admin")]
        public async Task<IActionResult> ObtenerGymAdminDashboard()
        {
            try
            {
                if (_userContext.EsSuperAdmin())
                    return Unauthorized(new { error = "Acceso denegado" });

                var stats = await _dashboardService.ObtenerGymAdminDashboard();
                return Ok(new
                {
                    message = "Dashboard obtenido exitosamente",
                    data = stats
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("super-admin")]
        public async Task<IActionResult> ObtenerSuperAdminDashboard()
        {
            try
            {
                if (_userContext.EsAdminGimnasio())
                    return Unauthorized(new { error = "Acceso denegado" });

                var stats = await _dashboardService.ObtenerSuperAdminDashboard();
                return Ok(new
                {
                    message = "Dashboard obtenido exitosamente",
                    data = stats
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
