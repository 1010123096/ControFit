using ControlFit.Application.CasosUso.CRUDMembresia;
using ControlFit.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControlFit.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar membresías.
    /// Todos los endpoints requieren autenticación.
    /// Super Admin puede ver todas las membresías.
    /// Admin de Gimnasio solo puede ver/crear/editar membresías de su gimnasio.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MembresiaController : ControllerBase
    {
        private readonly MembresiaService _service;

        public MembresiaController(MembresiaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Crea una nueva membresía.
        /// Solo Admin de Gimnasio puede crear membresías.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<MembresiaDTO>> Crear([FromBody] CrearMembresiaDTO dto)
        {
            try
            {
                var membresia = await _service.Crear(dto);
                return Ok(new
                {
                    message = "Membresía creada exitosamente",
                    data = new MembresiaDTO(
                        membresia.Id,
                        membresia.Nombre,
                        membresia.Duración,
                        membresia.Precio,
                        membresia.Estado,
                        membresia.MaximoIngresosPorDia ?? 0,
                        membresia.MaximoIngresosPorSemana ?? 0,
                        membresia.MaximoIngresosTotales,
                        membresia.GimnasioId
                    )
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene todas las membresías.
        /// Super Admin obtiene todas.
        /// Admin de Gimnasio obtiene solo las de su gimnasio.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> ObtenerTodas()
        {
            try
            {
                var membresias = await _service.ListarTodos();
                var dtos = membresias.Select(m => new MembresiaDTO(
                    m.Id,
                    m.Nombre,
                    m.Duración,
                    m.Precio,
                    m.Estado,
                    m.MaximoIngresosPorDia ?? 0,
                    m.MaximoIngresosPorSemana ?? 0,
                    m.MaximoIngresosTotales,
                    m.GimnasioId
                )).ToList();

                return Ok(new
                {
                    message = "Membresías obtenidas exitosamente",
                    data = dtos,
                    total = dtos.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene una membresía por su ID.
        /// Valida permisos según el gimnasio asignado.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<MembresiaDTO>> ObtenerPorId(int id)
        {
            try
            {
                var membresia = await _service.ObtenerPorId(id);
                return Ok(new
                {
                    message = "Membresía obtenida exitosamente",
                    data = new MembresiaDTO(
                        membresia.Id,
                        membresia.Nombre,
                        membresia.Duración,
                        membresia.Precio,
                        membresia.Estado,
                        membresia.MaximoIngresosPorDia ?? 0,
                        membresia.MaximoIngresosPorSemana ?? 0,
                        membresia.MaximoIngresosTotales,
                        membresia.GimnasioId
                    )
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza una membresía.
        /// Solo Super Admin puede editar membresías.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<MembresiaDTO>> Actualizar(int id, [FromBody] ActualizarMembresiaDTO dto)
        {
            try
            {
                dto.Id = id;
                var actualizado = await _service.Actualizar(dto);

                if (!actualizado)
                    return BadRequest(new { error = "No se pudo actualizar la membresía" });

                var membresia = await _service.ObtenerPorId(id);
                return Ok(new
                {
                    message = "Membresía actualizada exitosamente",
                    data = new MembresiaDTO(
                        membresia.Id,
                        membresia.Nombre,
                        membresia.Duración,
                        membresia.Precio,
                        membresia.Estado,
                        membresia.MaximoIngresosPorDia ?? 0,
                        membresia.MaximoIngresosPorSemana ?? 0,
                        membresia.MaximoIngresosTotales,
                        membresia.GimnasioId
                    )
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina una membresía.
        /// Solo Super Admin puede eliminar membresías.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            try
            {
                await _service.Eliminar(id);
                return Ok(new { message = "Membresía eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
