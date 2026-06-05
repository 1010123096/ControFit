using ControlFit.Application.CasosUso.CRUDAsignacion;
using ControlFit.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/asignacion")]
    public class AsignacionMembresiaController : Controller
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
            var resultado = await _service.Crear(dto);

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _service.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            return Ok(await _service.ObtenerPorId(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.Eliminar(id);

            return Ok();
        }
    }
}
