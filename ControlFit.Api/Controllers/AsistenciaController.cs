using ControlFit.Application.CasosUso.Ingreso;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlFit.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar asistencias (ingresos al gimnasio).
    /// Todos los endpoints requieren autenticación.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AsistenciaController : ControllerBase
    {
        private readonly RegistrarIngreso _registrarIngreso;
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IMiembroRepository _miembroRepository;
        private readonly IUserContextService _userContext;

        public AsistenciaController(
            RegistrarIngreso registrarIngreso,
            IAsistenciaRepository asistenciaRepository,
            IMiembroRepository miembroRepository,
            IUserContextService userContext)
        {
            _registrarIngreso = registrarIngreso;
            _asistenciaRepository = asistenciaRepository;
            _miembroRepository = miembroRepository;
            _userContext = userContext;
        }

        /// <summary>
        /// Registra el ingreso de un miembro al gimnasio.
        /// Valida que la membresía esté vigente y respeta límites de ingreso.
        /// </summary>
        [HttpPost("registrar")]
        public async Task<ActionResult> RegistrarIngreso([FromBody] RegistroIngresoDTO dto)
        {
            try
            {
                if (dto == null || dto.MiembroId <= 0)
                    return BadRequest(new { error = "ID de miembro inválido" });

                await _registrarIngreso.EjecutarAsync(dto.MiembroId);

                return Ok(new
                {
                    message = "Ingreso registrado exitosamente",
                    data = new { miembroId = dto.MiembroId, fechaHora = DateTime.Now }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene todas las asistencias.
        /// Super Admin obtiene todas.
        /// Admin de Gimnasio obtiene solo las de su gimnasio.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> ObtenerAsistencias()
        {
            try
            {
                var gimnasioId = _userContext.GetGimnasioId();
                List<dynamic> asistencias;

                if (_userContext.EsSuperAdmin())
                {
                    asistencias = await _asistenciaRepository.ObtenerTodosAsync()
                        .ContinueWith(async t =>
                        {
                            var lista = await t;
                            var dtos = new List<dynamic>();
                            foreach (var a in lista)
                            {
                                var miembro = await _miembroRepository.ObtenerPorIdAsync(a.MiembroId);
                                dtos.Add(new
                                {
                                    id = a.Id,
                                    miembroId = a.MiembroId,
                                    nombreMiembro = miembro?.Nombre,
                                    asignacionMembresiaId = a.AsignacionMembresiaId,
                                    fechaHoraAcceso = a.FechaHoraAcceso
                                });
                            }
                            return dtos;
                        }).Result;
                }
                else
                {
                    asistencias = await _asistenciaRepository.ObtenerTodosPorGimnasio(gimnasioId)
                        .ContinueWith(async t =>
                        {
                            var lista = await t;
                            var dtos = new List<dynamic>();
                            foreach (var a in lista)
                            {
                                var miembro = await _miembroRepository.ObtenerPorIdAsync(a.MiembroId);
                                dtos.Add(new
                                {
                                    id = a.Id,
                                    miembroId = a.MiembroId,
                                    nombreMiembro = miembro?.Nombre,
                                    asignacionMembresiaId = a.AsignacionMembresiaId,
                                    fechaHoraAcceso = a.FechaHoraAcceso
                                });
                            }
                            return dtos;
                        }).Result;
                }

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

        /// <summary>
        /// Filtra asistencias por miembro y rango de fechas.
        /// Valida permisos del gimnasio.
        /// </summary>
        [HttpGet("filtrar")]
        public async Task<ActionResult> FiltrarAsistencias(
            [FromQuery] int? miembroId = null,
            [FromQuery] DateTime? fechaInicio = null,
            [FromQuery] DateTime? fechaFin = null)
        {
            try
            {
                var gimnasioId = _userContext.GetGimnasioId();

                // Si se especifica un miembro, validar que pertenece al gimnasio del usuario (si no es Super Admin)
                if (miembroId.HasValue && _userContext.EsAdminGimnasio())
                {
                    var miembro = await _miembroRepository.ObtenerPorIdValidandoGimnasio(miembroId.Value, gimnasioId);
                    if (miembro == null)
                        return Unauthorized(new { error = "No tiene permiso para ver asistencias de este miembro" });
                }

                // Establecer fechas por defecto
                var inicio = fechaInicio ?? DateTime.Now.AddDays(-30);
                var fin = fechaFin ?? DateTime.Now.AddDays(1);

                List<dynamic> resultado = new List<dynamic>();

                if (miembroId.HasValue)
                {
                    var asistencias = await _asistenciaRepository.ObtenerPorMiembroEnRango(
                        miembroId.Value,
                        inicio,
                        fin,
                        gimnasioId
                    );

                    var miembro = await _miembroRepository.ObtenerPorIdAsync(miembroId.Value);
                    foreach (var a in asistencias)
                    {
                        resultado.Add(new
                        {
                            id = a.Id,
                            miembroId = a.MiembroId,
                            nombreMiembro = miembro?.Nombre,
                            asignacionMembresiaId = a.AsignacionMembresiaId,
                            fechaHoraAcceso = a.FechaHoraAcceso
                        });
                    }
                }
                else
                {
                    var todas = _userContext.EsSuperAdmin()
                        ? await _asistenciaRepository.ObtenerTodosAsync()
                        : await _asistenciaRepository.ObtenerTodosPorGimnasio(gimnasioId);

                    var filtradas = todas
                        .Where(a => a.FechaHoraAcceso >= inicio && a.FechaHoraAcceso <= fin)
                        .ToList();

                    foreach (var a in filtradas)
                    {
                        var miembro = await _miembroRepository.ObtenerPorIdAsync(a.MiembroId);
                        resultado.Add(new
                        {
                            id = a.Id,
                            miembroId = a.MiembroId,
                            nombreMiembro = miembro?.Nombre,
                            asignacionMembresiaId = a.AsignacionMembresiaId,
                            fechaHoraAcceso = a.FechaHoraAcceso
                        });
                    }
                }

                return Ok(new
                {
                    message = "Asistencias filtradas exitosamente",
                    filtros = new
                    {
                        miembroId = miembroId,
                        fechaInicio = inicio,
                        fechaFin = fin
                    },
                    data = resultado,
                    total = resultado.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
