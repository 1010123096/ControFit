using ControlFit.Application.CasosUso.Auditoria;
using ControlFit.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    [ApiController]
    [Route("api/auditoria")]
    [Authorize(Policy = "SuperAdmin")]
    public class AuditoriaController : ControllerBase
    {
        private readonly ConsultarAuditoria _consultar;

        public AuditoriaController(ConsultarAuditoria consultar)
        {
            _consultar = consultar;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            [FromQuery] int? gimnasioId = null,
            [FromQuery] string? accion = null,
            [FromQuery] DateTime? fechaInicio = null,
            [FromQuery] DateTime? fechaFin = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var result = await _consultar.EjecutarAsync(
                    gimnasioId, accion, fechaInicio, fechaFin, page, pageSize);

                return Ok(new
                {
                    message = "Registros de auditoría obtenidos",
                    data = result.Items,
                    total = result.Total,
                    page = result.Page,
                    pageSize = result.PageSize
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
