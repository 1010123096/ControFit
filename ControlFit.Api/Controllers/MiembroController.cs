using ControlFit.Application.CasosUso.CRUDMiembro;
using ControlFit.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/miembros")]
    [Authorize]
    public class MiembroController : ControllerBase
    {
        private readonly MiembroService _miembro;
        public MiembroController(MiembroService miembro)
        {
            _miembro = miembro;
        }

        private static MiembroDTO ToDTO(ControlFit.Domain.Entidad.Miembro m)
        {
            return new MiembroDTO
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Correo = m.Correo,
                Telefono = m.Telefono,
                FechaNacimiento = m.FechaNacimiento,
                GimnasioId = m.GimnasioId,
            };
        }

        [HttpPost("Registro")]
        public async Task<IActionResult> Crear([FromBody] MiembroDTO mdto)
        {
            try
            {
                var miembro = await _miembro.Crearmiembro(mdto);
                return Ok(new
                {
                    message = "Miembro creado exitosamente",
                    data = ToDTO(miembro)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("obtenerTodos")]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var miembros = await _miembro.ListarMiembro();
                var dtos = miembros.Select(ToDTO).ToList();
                return Ok(new
                {
                    message = "Miembros obtenidos exitosamente",
                    data = dtos,
                    total = dtos.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("obtenerporID")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var miembro = await _miembro.ObtenerMiembroId(id);
                return Ok(new
                {
                    message = "Miembro obtenido exitosamente",
                    data = ToDTO(miembro)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] MiembroDTO miembroDTO)
        {
            try
            {
                var actualizado = await _miembro.ActualizarMiembro(miembroDTO.Id, miembroDTO.Nombre, miembroDTO.Correo, miembroDTO.Telefono);
                if (!actualizado)
                    return BadRequest(new { error = "No se pudo actualizar el miembro" });
                var miembro = await _miembro.ObtenerMiembroId(miembroDTO.Id);
                return Ok(new
                {
                    message = "Miembro actualizado exitosamente",
                    data = ToDTO(miembro)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _miembro.EliminarMiembro(id);
                return Ok(new { message = "Miembro eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

