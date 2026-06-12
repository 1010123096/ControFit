using ControlFit.Application.CasosUso.Ingreso;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AnyAdmin")]
    public class AsistenciaController : ControllerBase
    {
        private readonly RegistrarIngreso _registrarIngreso;
        private readonly PreviewIngreso _previewIngreso;
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IMiembroRepository _miembroRepository;
        private readonly IUserContextService _userContext;

        public AsistenciaController(
            RegistrarIngreso registrarIngreso,
            PreviewIngreso previewIngreso,
            IAsistenciaRepository asistenciaRepository,
            IMiembroRepository miembroRepository,
            IUserContextService userContext)
        {
            _registrarIngreso = registrarIngreso;
            _previewIngreso = previewIngreso;
            _asistenciaRepository = asistenciaRepository;
            _miembroRepository = miembroRepository;
            _userContext = userContext;
        }

        [HttpGet("preview/{miembroId:int}")]
        public async Task<ActionResult> PreviewIngreso(int miembroId)
        {
            if (miembroId <= 0)
                return BadRequest(new { error = "ID de miembro inválido" });

            var preview = await _previewIngreso.EjecutarAsync(miembroId);
            if (preview == null)
                return NotFound(new { error = "Miembro no encontrado o sin permiso" });

            return Ok(new { message = "Preview de ingreso", data = preview });
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> RegistrarIngreso([FromBody] RegistroIngresoDTO dto)
        {
            try
            {
                if (dto == null || dto.MiembroId <= 0)
                    return BadRequest(new { error = "ID de miembro inválido" });

                await _registrarIngreso.EjecutarAsync(dto.MiembroId, dto.Fuente, dto.BiometricEventId);

                return Ok(new
                {
                    message = "Ingreso registrado exitosamente",
                    data = new { miembroId = dto.MiembroId, fuente = dto.Fuente, fechaHora = DateTime.Now }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerAsistencias()
        {
            try
            {
                var gimnasioId = _userContext.GetGimnasioId();
                var lista = _userContext.EsSuperAdmin()
                    ? await _asistenciaRepository.ObtenerTodosAsync()
                    : await _asistenciaRepository.ObtenerTodosPorGimnasio(gimnasioId);

                var asistencias = await MapAsistenciasAsync(lista);

                return Ok(new
                {
                    message = "Asistencias obtenidas exitosamente",
                    data = asistencias,
                    total = asistencias.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult> FiltrarAsistencias(
            [FromQuery] int? miembroId = null,
            [FromQuery] DateTime? fechaInicio = null,
            [FromQuery] DateTime? fechaFin = null)
        {
            try
            {
                var gimnasioId = _userContext.GetGimnasioId();

                if (miembroId.HasValue && _userContext.EsAdminGimnasio())
                {
                    var miembro = await _miembroRepository.ObtenerPorIdValidandoGimnasio(miembroId.Value, gimnasioId);
                    if (miembro == null)
                        return Unauthorized(new { error = "No tiene permiso para ver asistencias de este miembro" });
                }

                var inicio = fechaInicio ?? DateTime.Now.AddDays(-30);
                var fin = fechaFin ?? DateTime.Now.AddDays(1);
                List<Asistencia> filtradas;

                if (miembroId.HasValue)
                {
                    filtradas = await _asistenciaRepository.ObtenerPorMiembroEnRango(miembroId.Value, inicio, fin, gimnasioId);
                }
                else
                {
                    var todas = _userContext.EsSuperAdmin()
                        ? await _asistenciaRepository.ObtenerTodosAsync()
                        : await _asistenciaRepository.ObtenerTodosPorGimnasio(gimnasioId);

                    filtradas = todas
                        .Where(a => a.FechaHoraAcceso >= inicio && a.FechaHoraAcceso <= fin)
                        .ToList();
                }

                var resultado = await MapAsistenciasAsync(filtradas);

                return Ok(new
                {
                    message = "Asistencias filtradas exitosamente",
                    filtros = new { miembroId, fechaInicio = inicio, fechaFin = fin },
                    data = resultado,
                    total = resultado.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        private async Task<List<object>> MapAsistenciasAsync(List<Asistencia> lista)
        {
            var dtos = new List<object>();
            foreach (var a in lista)
            {
                var miembro = await _miembroRepository.ObtenerPorIdAsync(a.MiembroId);
                dtos.Add(new
                {
                    id = a.Id,
                    miembroId = a.MiembroId,
                    nombreMiembro = miembro?.Nombre,
                    asignacionMembresiaId = a.AsignacionMembresiaId,
                    fechaHoraAcceso = a.FechaHoraAcceso,
                    fuente = a.Fuente.ToString(),
                    biometricEventId = a.BiometricEventId
                });
            }
            return dtos;
        }
    }
}
