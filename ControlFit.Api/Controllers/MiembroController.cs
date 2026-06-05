using ControlFit.Application.CasosUso.CRUDMiembro;
using ControlFit.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/miembros")]
    [Authorize]
    public class MiembroController : Controller
    {
        private readonly MiembroService _miembro;
        public MiembroController(MiembroService miembro)
        {
            _miembro = miembro;
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
                    data = miembro
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
                return Ok(new
                {
                    message = "Miembros obtenidos exitosamente",
                    data = miembros,
                    total = miembros.Count
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
                    data = miembro
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
                var miembro = await _miembro.ActualizarMiembro(miembroDTO.Id, miembroDTO.Nombre, miembroDTO.Correo, miembroDTO.Telefono);
                return Ok(new
                {
                    message = "Miembro actualizado exitosamente",
                    data = miembro
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

