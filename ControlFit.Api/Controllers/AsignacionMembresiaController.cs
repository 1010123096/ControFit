using ControlFit.Application.CasosUso.CRUDAsignacion;
using ControlFit.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/asignacion")]
    [Authorize]
    public class AsignacionMembresiaController : ControllerBase
    {
        private readonly AsignacionMembresiaService _service;

        public AsignacionMembresiaController(
            AsignacionMembresiaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(
            [FromBody] AsignacionMembresiaCrearDTO dto)
        {
            try
            {
                var resultado = await _service.Crear(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                return Ok(await _service.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                return Ok(await _service.ObtenerPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _service.Eliminar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
