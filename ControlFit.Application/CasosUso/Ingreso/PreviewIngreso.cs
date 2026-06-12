using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;

namespace ControlFit.Application.CasosUso.Ingreso
{
    public class PreviewIngreso
    {
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IAsignacionMembresiaRepository _asignacionRepository;
        private readonly IMembresiaRepository _membresiaRepository;
        private readonly IMiembroRepository _miembroRepository;
        private readonly IUserContextService _userContext;

        public PreviewIngreso(
            IAsistenciaRepository asistenciaRepository,
            IAsignacionMembresiaRepository asignacionRepository,
            IMembresiaRepository membresiaRepository,
            IMiembroRepository miembroRepository,
            IUserContextService userContext)
        {
            _asistenciaRepository = asistenciaRepository;
            _asignacionRepository = asignacionRepository;
            _membresiaRepository = membresiaRepository;
            _miembroRepository = miembroRepository;
            _userContext = userContext;
        }

        public async Task<PreviewIngresoDTO?> EjecutarAsync(int miembroId)
        {
            var gimnasioId = _userContext.GetGimnasioId();
            Miembro? miembro;

            if (_userContext.EsAdminGimnasio())
            {
                miembro = await _miembroRepository.ObtenerPorIdValidandoGimnasio(miembroId, gimnasioId);
                if (miembro == null) return null;
            }
            else
            {
                miembro = await _miembroRepository.ObtenerPorIdAsync(miembroId);
                if (miembro == null) return null;
            }

            var preview = new PreviewIngresoDTO
            {
                MiembroId = miembro.Id,
                NombreMiembro = miembro.Nombre
            };

            var asignacion = await _asignacionRepository.ObtenerActivaAsync(miembroId);
            if (asignacion == null || !asignacion.EstaVigente())
            {
                preview.PuedeIngresar = false;
                preview.MotivoBloqueo = "Membresía vencida o sin asignación activa";
                return preview;
            }

            var membresia = await _membresiaRepository.ObtenerPorIdAsync(asignacion.MembresiaId);
            preview.NombreMembresia = membresia?.Nombre;
            preview.FechaVencimiento = asignacion.FechaFin;

            preview.YaIngresoHoy = await _asistenciaRepository.YaIngresoHoyAsync(miembroId);
            preview.IngresosSemana = await _asistenciaRepository.ObtenerIngresosSemanaAsync(miembroId);
            preview.MaximoIngresosSemana = membresia?.MaximoIngresosPorSemana;

            if (preview.YaIngresoHoy)
            {
                preview.PuedeIngresar = false;
                preview.MotivoBloqueo = "El miembro ya ingresó hoy";
                return preview;
            }

            if (membresia?.MaximoIngresosPorSemana.HasValue == true &&
                preview.IngresosSemana >= membresia.MaximoIngresosPorSemana.Value)
            {
                preview.PuedeIngresar = false;
                preview.MotivoBloqueo = "Límite semanal alcanzado";
                return preview;
            }

            preview.PuedeIngresar = true;
            return preview;
        }
    }
}
