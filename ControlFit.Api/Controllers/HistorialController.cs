using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
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
    /// Controlador para consultar historial de actividades de miembros.
    /// Proporciona vistas consolidadas de:
    /// - Historial de membresías asignadas
    /// - Historial de asignaciones
    /// - Historial de asistencias
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HistorialController : ControllerBase
    {
        private const string MiembroInvalido = "ID de miembro inválido";
        private const string MiembroNoEncontrado = "Miembro no encontrado";
        private const string SinPermisoHistorial = "No tiene permiso para ver el historial de este miembro";
        private readonly IMiembroRepository _miembroRepository;
        private readonly IMembresiaRepository _membresiaRepository;
        private readonly IAsignacionMembresiaRepository _asignacionRepository;
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IUserContextService _userContext;

        public HistorialController(
            IMiembroRepository miembroRepository,
            IMembresiaRepository membresiaRepository,
            IAsignacionMembresiaRepository asignacionRepository,
            IAsistenciaRepository asistenciaRepository,
            IUserContextService userContext)
        {
            _miembroRepository = miembroRepository;
            _membresiaRepository = membresiaRepository;
            _asignacionRepository = asignacionRepository;
            _asistenciaRepository = asistenciaRepository;
            _userContext = userContext;
        }

        /// <summary>
        /// Obtiene todos los historiales (endpoint unificado).
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> ObtenerTodos()
        {
            try
            {
                var gimnasioId = _userContext.GetGimnasioId();

                var miembros = await _miembroRepository.ListarTodos();
                if (_userContext.EsAdminGimnasio())
                    miembros = miembros.Where(m => m.GimnasioId == gimnasioId).ToList();

                var historial = new List<dynamic>();
                foreach (var m in miembros)
                {
                    var scopeGimnasioId = _userContext.EsAdminGimnasio() ? gimnasioId : m.GimnasioId;
                    var asignaciones = await _asignacionRepository.ObtenerPorMiembroGimnasio(m.Id, scopeGimnasioId);
                    var asistencias = await _asistenciaRepository.ObtenerPorMiembroEnRango(
                        m.Id, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), scopeGimnasioId);

                    foreach (var a in asignaciones)
                    {
                        historial.Add(new
                        {
                            id = a.Id,
                            miembroId = m.Id,
                            accion = $"Asignación de membresía",
                            detalle = $"Miembro: {m.Nombre}",
                            fecha = a.FechaAsignacion
                        });
                    }
                    foreach (var a in asistencias)
                    {
                        historial.Add(new
                        {
                            id = a.Id + 10000,
                            miembroId = m.Id,
                            accion = "Ingreso al gimnasio",
                            detalle = $"Miembro: {m.Nombre}",
                            fecha = a.FechaHoraAcceso
                        });
                    }
                }

                historial = historial.OrderByDescending(h => (DateTime)h.fecha).ToList();

                return Ok(new
                {
                    message = "Historial obtenido exitosamente",
                    data = historial,
                    total = historial.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene el historial de membresías asignadas a un miembro.
        /// Valida que el usuario tenga permisos para acceder a ese miembro.
        /// </summary>
        [HttpGet("membresias")]
        public async Task<ActionResult> ObtenerHistorialMembresias([FromQuery] int miembroId)
        {
            try
            {
                if (miembroId <= 0)
                    return BadRequest(new { error = MiembroInvalido });

                var gimnasioId = _userContext.GetGimnasioId();

                Miembro? miembro;
                if (_userContext.EsAdminGimnasio())
                {
                    miembro = await _miembroRepository.ObtenerPorIdValidandoGimnasio(miembroId, gimnasioId);
                    if (miembro == null)
                        return Unauthorized(new { error = SinPermisoHistorial });
                }
                else
                {
                    miembro = await _miembroRepository.ObtenerPorIdAsync(miembroId);
                    if (miembro == null)
                        return NotFound(new { error = MiembroNoEncontrado });
                }

                var scopeGimnasioId = _userContext.EsAdminGimnasio() ? gimnasioId : miembro.GimnasioId;
                var asignaciones = await _asignacionRepository.ObtenerPorMiembroGimnasio(miembroId, scopeGimnasioId);

                var historialMembresias = new List<dynamic>();
                foreach (var asignacion in asignaciones)
                {
                    var membresia = await _membresiaRepository.ObtenerPorIdAsync(asignacion.MembresiaId);
                    historialMembresias.Add(new
                    {
                        asignacionId = asignacion.Id,
                        membresia = new
                        {
                            id = membresia?.Id,
                            nombre = membresia?.Nombre,
                            duracion = membresia?.Duración,
                            precio = membresia?.Precio
                        },
                        fechaAsignacion = asignacion.FechaAsignacion,
                        fechaVencimiento = asignacion.FechaVencimiento,
                        activa = asignacion.EstaVigente()
                    });
                }

                return Ok(new
                {
                    message = "Historial de membresías obtenido exitosamente",
                    miembroId = miembroId,
                    data = historialMembresias,
                    total = historialMembresias.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene el historial de asignaciones de membresías para un miembro.
        /// </summary>
        [HttpGet("asignaciones")]
        public async Task<ActionResult> ObtenerHistorialAsignaciones([FromQuery] int miembroId)
        {
            try
            {
                if (miembroId <= 0)
                    return BadRequest(new { error = MiembroInvalido });

                var gimnasioId = _userContext.GetGimnasioId();

                Miembro? miembro;
                if (_userContext.EsAdminGimnasio())
                {
                    miembro = await _miembroRepository.ObtenerPorIdValidandoGimnasio(miembroId, gimnasioId);
                    if (miembro == null)
                        return Unauthorized(new { error = SinPermisoHistorial });
                }
                else
                {
                    miembro = await _miembroRepository.ObtenerPorIdAsync(miembroId);
                    if (miembro == null)
                        return NotFound(new { error = MiembroNoEncontrado });
                }

                var scopeGimnasioId = _userContext.EsAdminGimnasio() ? gimnasioId : miembro.GimnasioId;
                var asignaciones = await _asignacionRepository.ObtenerPorMiembroGimnasio(miembroId, scopeGimnasioId);

                var historialAsignaciones = asignaciones.Select(a => new
                {
                    id = a.Id,
                    miembroId = a.MiembroId,
                    membresiaId = a.MembresiaId,
                    fechaAsignacion = a.FechaAsignacion,
                    fechaVencimiento = a.FechaVencimiento,
                    activa = a.EstaVigente(),
                    diasRestantes = (a.FechaVencimiento - DateTime.Now).Days
                }).ToList();

                return Ok(new
                {
                    message = "Historial de asignaciones obtenido exitosamente",
                    miembroId = miembroId,
                    data = historialAsignaciones,
                    total = historialAsignaciones.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene el historial de asistencias (ingresos) de un miembro.
        /// </summary>
        [HttpGet("asistencias")]
        public async Task<ActionResult> ObtenerHistorialAsistencias(
            [FromQuery] int miembroId,
            [FromQuery] DateTime? fechaInicio = null,
            [FromQuery] DateTime? fechaFin = null)
        {
            try
            {
                if (miembroId <= 0)
                    return BadRequest(new { error = MiembroInvalido });

                var gimnasioId = _userContext.GetGimnasioId();

                Miembro? miembro;
                if (_userContext.EsAdminGimnasio())
                {
                    miembro = await _miembroRepository.ObtenerPorIdValidandoGimnasio(miembroId, gimnasioId);
                    if (miembro == null)
                        return Unauthorized(new { error = SinPermisoHistorial });
                }
                else
                {
                    miembro = await _miembroRepository.ObtenerPorIdAsync(miembroId);
                    if (miembro == null)
                        return NotFound(new { error = MiembroNoEncontrado });
                }

                var scopeGimnasioId = _userContext.EsAdminGimnasio() ? gimnasioId : miembro.GimnasioId;

                // Establecer rango de fechas
                var inicio = fechaInicio ?? DateTime.Now.AddMonths(-1);
                var fin = (fechaFin ?? DateTime.Now).AddDays(1);

                var asistencias = await _asistenciaRepository.ObtenerPorMiembroEnRango(
                    miembroId,
                    inicio,
                    fin,
                    scopeGimnasioId
                );

                var historialAsistencias = asistencias
                    .OrderByDescending(a => a.FechaHoraAcceso)
                    .Select(a => new
                    {
                        id = a.Id,
                        miembroId = a.MiembroId,
                        asignacionMembresiaId = a.AsignacionMembresiaId,
                        fechaHoraAcceso = a.FechaHoraAcceso,
                        hora = a.FechaHoraAcceso.ToString("HH:mm"),
                        fecha = a.FechaHoraAcceso.ToString("yyyy-MM-dd")
                    })
                    .ToList();

                // Estadísticas
                var estadisticas = new
                {
                    totalIngresos = historialAsistencias.Count,
                    ingresosPorMes = historialAsistencias
                        .GroupBy(a => a.fecha.Substring(0, 7))
                        .Select(g => new { mes = g.Key, cantidad = g.Count() })
                        .ToList(),
                    ultimoIngreso = historialAsistencias.FirstOrDefault()?.fechaHoraAcceso
                };

                return Ok(new
                {
                    message = "Historial de asistencias obtenido exitosamente",
                    miembroId = miembroId,
                    filtros = new
                    {
                        fechaInicio = inicio,
                        fechaFin = fin
                    },
                    estadisticas = estadisticas,
                    data = historialAsistencias,
                    total = historialAsistencias.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene un resumen completo del miembro (datos personales + estado de membresía actual).
        /// </summary>
        [HttpGet("miembro/{miembroId}")]
        public async Task<ActionResult> ObtenerResumenMiembro(int miembroId)
        {
            try
            {
                if (miembroId <= 0)
                    return BadRequest(new { error = MiembroInvalido });

                var gimnasioId = _userContext.GetGimnasioId();

                // Obtener miembro
                var miembro = _userContext.EsAdminGimnasio()
                    ? await _miembroRepository.ObtenerPorIdValidandoGimnasio(miembroId, gimnasioId)
                    : await _miembroRepository.ObtenerPorIdAsync(miembroId);

                if (miembro == null)
                    return NotFound(new { error = MiembroNoEncontrado });

                // Obtener asignación activa
                var asignacionActiva = await _asignacionRepository.ObtenerActivaAsync(miembroId);
                var membresiaActiva = asignacionActiva != null
                    ? await _membresiaRepository.ObtenerPorIdAsync(asignacionActiva.MembresiaId)
                    : null;

                var membresiaInfo = membresiaActiva != null
                    ? new
                    {
                        id = membresiaActiva.Id,
                        nombre = membresiaActiva.Nombre,
                        duracion = membresiaActiva.Duración,
                        precio = membresiaActiva.Precio,
                        fechaVencimiento = asignacionActiva?.FechaVencimiento,
                        diasRestantes = asignacionActiva != null
                            ? (asignacionActiva.FechaVencimiento - DateTime.Now).Days
                            : 0
                    }
                    : null;

                // Obtener últimas asistencias
                var asistenciasRecientes = await _asistenciaRepository.ObtenerPorMiembroEnRango(
                    miembroId,
                    DateTime.Now.AddDays(-7),
                    DateTime.Now.AddDays(1),
                    miembro.GimnasioId
                );

                return Ok(new
                {
                    message = "Resumen del miembro obtenido exitosamente",
                    data = new
                    {
                        miembro = new
                        {
                            id = miembro.Id,
                            nombre = miembro.Nombre,
                            correo = miembro.Correo,
                            telefono = miembro.Telefono,
                            fechaNacimiento = miembro.FechaNacimiento,
                            estado = miembro.Estado,
                            gimnasioId = miembro.GimnasioId
                        },
                        membresiaActual = membresiaInfo,
                        estadisticas = new
                        {
                            ingresosSemanal = asistenciasRecientes.Count,
                            ultimoIngreso = asistenciasRecientes.OrderByDescending(a => a.FechaHoraAcceso).FirstOrDefault()?.FechaHoraAcceso
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
