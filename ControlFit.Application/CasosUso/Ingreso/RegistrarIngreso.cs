using ControlFit.Application.Servicios;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;

namespace ControlFit.Application.CasosUso.Ingreso
{
    public class RegistrarIngreso
    {
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IAsignacionMembresiaRepository _asignacionRepository;
        private readonly IMembresiaRepository _membresiaRepository;
        private readonly IMiembroRepository _miembroRepository;
        private readonly IUserContextService _userContext;
        private readonly AuditService _auditService;

        public RegistrarIngreso(
            IAsistenciaRepository asistenciaRepository,
            IAsignacionMembresiaRepository asignacionRepository,
            IMembresiaRepository membresiaRepository,
            IMiembroRepository miembroRepository,
            IUserContextService userContext,
            AuditService auditService)
        {
            _asistenciaRepository = asistenciaRepository;
            _asignacionRepository = asignacionRepository;
            _membresiaRepository = membresiaRepository;
            _miembroRepository = miembroRepository;
            _userContext = userContext;
            _auditService = auditService;
        }

        public async Task EjecutarAsync(int miembroId, FuenteAsistencia fuente = FuenteAsistencia.Manual, int? biometricEventId = null)
        {
            var gimnasioId = _userContext.GetGimnasioId();

            if (_userContext.EsAdminGimnasio())
            {
                var miembro = await _miembroRepository.ObtenerPorIdValidandoGimnasio(miembroId, gimnasioId);
                if (miembro == null)
                    throw new DomainException("No tiene permiso para registrar ingreso de este miembro.");
            }

            var asignacion = await _asignacionRepository.ObtenerActivaAsync(miembroId);
            if (asignacion == null || !asignacion.EstaVigente())
                throw new DomainException("Membresía vencida");

            if (await _asistenciaRepository.YaIngresoHoyAsync(miembroId))
                throw new DomainException("El miembro ya ingresó hoy");

            var membresia = await _membresiaRepository.ObtenerPorIdAsync(asignacion.MembresiaId);
            var ingresosSemana = await _asistenciaRepository.ObtenerIngresosSemanaAsync(miembroId);

            if (membresia?.MaximoIngresosPorSemana.HasValue == true &&
                ingresosSemana >= membresia.MaximoIngresosPorSemana.Value)
            {
                throw new DomainException("Límite semanal alcanzado");
            }

            if (fuente == FuenteAsistencia.Biometrica && !biometricEventId.HasValue)
                throw new DomainException("El ingreso biométrico requiere un evento asociado.");

            var asistencia = new Asistencia(miembroId, asignacion.Id, fuente, biometricEventId);
            await _asistenciaRepository.RegistrarAsync(asistencia);

            await _auditService.RegistrarAsync(
                "Asistencia.Registrada",
                "Asistencia",
                asistencia.Id.ToString(),
                $"Miembro {miembroId}, fuente {fuente}");
        }
    }
}
