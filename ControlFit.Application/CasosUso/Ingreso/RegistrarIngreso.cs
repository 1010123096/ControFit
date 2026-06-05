using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Application.CasosUso.Ingreso
{
    /// <summary>
    /// Caso de uso para registrar el ingreso de un miembro al gimnasio.
    /// Valida:
    /// 1. La membresía está asignada y vigente
    /// 2. El miembro no ha ingresado hoy
    /// 3. Límites de ingreso por semana no sean excedidos
    /// </summary>
    public class RegistrarIngreso
    {
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IAsignacionMembresiaRepository _asignacionRepository;
        private readonly IMembresiaRepository _membresiaRepository;
        private readonly IMiembroRepository _miembroRepository;
        private readonly IUserContextService _userContext;

        public RegistrarIngreso(
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

        /// <summary>
        /// Registra el ingreso de un miembro validando permisos y restricciones.
        /// </summary>
        public async Task EjecutarAsync(int miembroId)
        {
            var gimnasioId = _userContext.GetGimnasioId();

            // Validar que el miembro pertenece al gimnasio del usuario (si no es Super Admin)
            if (_userContext.EsAdminGimnasio())
            {
                var miembro = await _miembroRepository.ObtenerPorIdValidandoGimnasio(miembroId, gimnasioId);
                if (miembro == null)
                    throw new Exception("No tiene permiso para registrar ingreso de este miembro.");
            }

            var asignacion = await _asignacionRepository.ObtenerActivaAsync(miembroId);

            if (asignacion == null || !asignacion.EstaVigente())
                throw new Exception("Membresía vencida");

            var yaIngreso = await _asistenciaRepository.YaIngresoHoyAsync(miembroId);

            if (yaIngreso)
                throw new Exception("El miembro ya ingresó hoy");

            var membresia = await _membresiaRepository.ObtenerPorIdAsync(asignacion.MembresiaId);

            var ingresosSemana = await _asistenciaRepository.ObtenerIngresosSemanaAsync(miembroId);

            if (membresia.MaximoIngresosPorSemana.HasValue &&
                ingresosSemana >= membresia.MaximoIngresosPorSemana.Value)
            {
                throw new Exception("Límite semanal alcanzado");
            }

            var asistencia = new Asistencia(miembroId, asignacion.Id);

            await _asistenciaRepository.RegistrarAsync(asistencia);
        }
    }
}
