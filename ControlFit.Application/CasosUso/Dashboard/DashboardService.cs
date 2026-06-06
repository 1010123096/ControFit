using ControlFit.Application.Servicios;
using ControlFit.Domain.Interfaz_puertos_;
using ControlFit.Domain.Entidad;

namespace ControlFit.Application.CasosUso.Dashboard
{
    public class DashboardService
    {
        private readonly IMiembroRepository _miembroRepo;
        private readonly IAsistenciaRepository _asistenciaRepo;
        private readonly IAsignacionMembresiaRepository _asignacionRepo;
        private readonly IGimnasioRepository _gimnasioRepo;
        private readonly IAdministradorRepository _adminRepo;
        private readonly IUserContextService _userContext;

        public DashboardService(
            IMiembroRepository miembroRepo,
            IAsistenciaRepository asistenciaRepo,
            IAsignacionMembresiaRepository asignacionRepo,
            IGimnasioRepository gimnasioRepo,
            IAdministradorRepository adminRepo,
            IUserContextService userContext)
        {
            _miembroRepo = miembroRepo;
            _asistenciaRepo = asistenciaRepo;
            _asignacionRepo = asignacionRepo;
            _gimnasioRepo = gimnasioRepo;
            _adminRepo = adminRepo;
            _userContext = userContext;
        }

        public async Task<GymAdminDashboardDTO> ObtenerGymAdminDashboard()
        {
            var gimnasioId = _userContext.GetGimnasioId();

            var miembros = gimnasioId > 0
                ? await _miembroRepo.ListarTodosPorGimnasio(gimnasioId)
                : await _miembroRepo.ListarTodos();

            var miembrosActivos = miembros.Where(m => m.Estado).ToList();

            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            var asistencias = gimnasioId > 0
                ? await _asistenciaRepo.ObtenerTodosPorGimnasio(gimnasioId)
                : await _asistenciaRepo.ObtenerTodosAsync();

            var asistenciasHoy = asistencias.Count(a => a.FechaHoraAcceso >= hoy && a.FechaHoraAcceso < manana);

            var asignaciones = gimnasioId > 0
                ? await _asignacionRepo.ObtenerTodosPorGimnasio(gimnasioId)
                : await _asignacionRepo.ObtenerTodosAsync();

            var membresiasVencidas = asignaciones.Count(a => !a.EstaVigente());

            return new GymAdminDashboardDTO
            {
                TotalMiembros = miembros.Count,
                MiembrosActivos = miembrosActivos.Count,
                MembresiasVencidas = membresiasVencidas,
                AsistenciasHoy = asistenciasHoy
            };
        }

        public async Task<SuperAdminDashboardDTO> ObtenerSuperAdminDashboard()
        {
            var gimnasios = await _gimnasioRepo.ListarTodos();
            var miembros = await _miembroRepo.ListarTodos();
            var admins = await _adminRepo.ObtenerTodosAsync();

            return new SuperAdminDashboardDTO
            {
                TotalGimnasios = gimnasios.Count,
                TotalMiembros = miembros.Count,
                TotalAdministradores = admins.Count
            };
        }
    }

    public class GymAdminDashboardDTO
    {
        public int TotalMiembros { get; set; }
        public int MiembrosActivos { get; set; }
        public int MembresiasVencidas { get; set; }
        public int AsistenciasHoy { get; set; }
    }

    public class SuperAdminDashboardDTO
    {
        public int TotalGimnasios { get; set; }
        public int TotalMiembros { get; set; }
        public int TotalAdministradores { get; set; }
    }
}
