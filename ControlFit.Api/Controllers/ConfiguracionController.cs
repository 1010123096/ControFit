using ControlFit.Application.CasosUso.Configuracion;
using ControlFit.Application.DTO;
using ControlFit.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/configuracion")]
    [Authorize(Policy = "SuperAdmin")]
    public class ConfiguracionController : ControllerBase
    {
        private readonly ConfiguracionPlataformaService _service;

        public ConfiguracionController(ConfiguracionPlataformaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                var config = await _service.Obtener();
                return Ok(new
                {
                    message = "Configuración obtenida exitosamente",
                    data = config
                });
            }
            catch (DomainException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarConfiguracionPlataformaDTO dto)
        {
            try
            {
                var config = await _service.Actualizar(dto);
                return Ok(new
                {
                    message = "Configuración actualizada exitosamente",
                    data = config
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
