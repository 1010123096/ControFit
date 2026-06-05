using ControlFit.Application.CasosUso.CRUDGimnasio;
using ControlFit.Application.DTO;
using ControlFit.Domain.Entidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/gimnasios")]
    [Authorize]
    public class GimnasioController : Controller
    {
        private readonly GimnasioService _gimnasioService;

        public GimnasioController(GimnasioService gimnasioService)
        {
            _gimnasioService = gimnasioService;
        }

        [HttpPost("Registro")]
        public async Task<IActionResult> Crear([FromBody] GimnasioCrearDTO gimnasioDTO)
        {
            try
            {
                var gimnasio = await _gimnasioService.CrearGimnasio(gimnasioDTO);
                return Ok(new
                {
                    message = "Gimnasio creado exitosamente",
                    data = gimnasio
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
                var gimnasios = await _gimnasioService.BuscarTodos();
                return Ok(new
                {
                    message = "Gimnasios obtenidos exitosamente",
                    data = gimnasios,
                    total = gimnasios.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var gimnasio = await _gimnasioService.BuscarPorId(id);
                return Ok(new
                {
                    message = "Gimnasio obtenido exitosamente",
                    data = gimnasio
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] GimnasioActualizarDTO gimnasioDTO)
        {
            try
            {
                var resultado = await _gimnasioService.ActualizarGimnasio(gimnasioDTO);
                if (!resultado)
                    return BadRequest(new { error = "No se pudo actualizar el gimnasio" });

                var gimnasio = await _gimnasioService.BuscarPorId(gimnasioDTO.Id);
                return Ok(new
                {
                    message = "Gimnasio actualizado exitosamente",
                    data = gimnasio
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
                await _gimnasioService.EliminarGimnasio(id);
                return Ok(new { message = "Gimnasio eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
