using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;

namespace ControlFit.Application.CasosUso.Seed
{
    public class SeedDemoDataService
    {
        private readonly IMiembroRepository _miembroRepo;
        private readonly IMembresiaRepository _membresiaRepo;
        private readonly IAsignacionMembresiaRepository _asignacionRepo;
        private readonly IAsistenciaRepository _asistenciaRepo;

        public SeedDemoDataService(
            IMiembroRepository miembroRepo,
            IMembresiaRepository membresiaRepo,
            IAsignacionMembresiaRepository asignacionRepo,
            IAsistenciaRepository asistenciaRepo)
        {
            _miembroRepo = miembroRepo;
            _membresiaRepo = membresiaRepo;
            _asignacionRepo = asignacionRepo;
            _asistenciaRepo = asistenciaRepo;
        }

        public async Task<SeedDemoResult> SeedAsync(int gimnasioId)
        {
            var result = new SeedDemoResult();

            var membresiaMensual = await EnsureMembresiaAsync(
                gimnasioId, "Plan Mensual", 30, 120, 1, 5, 30);
            var membresiaTrimestral = await EnsureMembresiaAsync(
                gimnasioId, "Plan Trimestral", 90, 300, 1, 7, 90);
            result.Membresias = 2;

            var miembrosData = new[]
            {
                ("Ana García", "ana@cossgym.com", "999111222"),
                ("Carlos López", "carlos@cossgym.com", "999333444"),
                ("María Ruiz", "maria@cossgym.com", "999555666"),
                ("Jorge Mendoza", "jorge@cossgym.com", "999777888"),
                ("Lucía Torres", "lucia@cossgym.com", "999000111"),
            };

            var miembros = new List<Miembro>();
            foreach (var (nombre, correo, telefono) in miembrosData)
            {
                var miembro = await EnsureMiembroAsync(gimnasioId, nombre, correo, telefono);
                miembros.Add(miembro);
            }
            result.Miembros = miembros.Count;

            var planes = new[] { membresiaMensual, membresiaMensual, membresiaTrimestral, membresiaMensual, membresiaTrimestral };
            for (var i = 0; i < miembros.Count; i++)
            {
                var asignacion = await EnsureAsignacionAsync(miembros[i].Id, planes[i]);
                if (asignacion != null)
                    result.Asignaciones++;
            }

            var hoy = DateTime.Today;
            var asistenciasExistentes = await _asistenciaRepo.ObtenerTodosPorGimnasio(gimnasioId);
            if (asistenciasExistentes.Count == 0)
            {
                var asistenciasPlan = new[]
                {
                    (miembros[0].Id, hoy.AddDays(-2), FuenteAsistencia.Manual),
                    (miembros[0].Id, hoy.AddDays(-1), FuenteAsistencia.Manual),
                    (miembros[1].Id, hoy.AddDays(-1), FuenteAsistencia.Manual),
                    (miembros[2].Id, hoy, FuenteAsistencia.Manual),
                    (miembros[3].Id, hoy.AddDays(-3), FuenteAsistencia.Manual),
                };

                foreach (var (miembroId, fecha, fuente) in asistenciasPlan)
                {
                    if (await EnsureAsistenciaAsync(miembroId, fecha, fuente))
                        result.Asistencias++;
                }
            }
            else
            {
                result.Asistencias = asistenciasExistentes.Count;
            }

            return result;
        }

        private async Task<Membresia> EnsureMembresiaAsync(
            int gimnasioId, string nombre, int duracion, double precio,
            int maxDia, int maxSemana, int maxTotal)
        {
            var existentes = await _membresiaRepo.ListarTodosPorGimnasio(gimnasioId);
            var existente = existentes.FirstOrDefault(m =>
                string.Equals(m.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
            if (existente != null)
                return existente;

            var membresia = new Membresia(nombre, duracion, precio, maxDia, maxSemana, maxTotal, true, gimnasioId);
            return await _membresiaRepo.CrearAsync(membresia);
        }

        private async Task<Miembro> EnsureMiembroAsync(int gimnasioId, string nombre, string correo, string telefono)
        {
            var existentes = await _miembroRepo.ListarTodosPorGimnasio(gimnasioId);
            var existente = existentes.FirstOrDefault(m =>
                string.Equals(m.Correo, correo, StringComparison.OrdinalIgnoreCase));
            if (existente != null)
                return existente;

            var miembro = new Miembro(nombre, correo, telefono, new DateOnly(1995, 6, 15), gimnasioId);
            return await _miembroRepo.CrearAsync(miembro);
        }

        private async Task<AsignacionMembresia?> EnsureAsignacionAsync(int miembroId, Membresia membresia)
        {
            var activa = await _asignacionRepo.ObtenerActivaAsync(miembroId);
            if (activa != null)
                return null;

            var asignacion = new AsignacionMembresia(miembroId, membresia);
            return await _asignacionRepo.CrearAsync(asignacion);
        }

        private async Task<bool> EnsureAsistenciaAsync(int miembroId, DateTime fecha, FuenteAsistencia fuente)
        {
            if (fecha.Date == DateTime.Today)
            {
                var yaHoy = await _asistenciaRepo.YaIngresoHoyAsync(miembroId);
                if (yaHoy)
                    return false;
            }

            var asignacion = await _asignacionRepo.ObtenerActivaAsync(miembroId);
            if (asignacion == null)
                return false;

            var asistencia = new Asistencia(miembroId, asignacion.Id, fuente)
            {
                FechaHoraAcceso = fecha.Date.AddHours(9)
            };
            await _asistenciaRepo.RegistrarAsync(asistencia);
            return true;
        }
    }

    public class SeedDemoResult
    {
        public int Miembros { get; set; }
        public int Membresias { get; set; }
        public int Asignaciones { get; set; }
        public int Asistencias { get; set; }
    }
}
