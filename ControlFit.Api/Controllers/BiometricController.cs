using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlFit.Api.Controllers
{
    /// <summary>
    /// Stub de ingesta biométrica — implementación hardware en V4.
    /// </summary>
    [ApiController]
    [Route("api/biometric")]
    [Authorize(Policy = "AnyAdmin")]
    public class BiometricController : ControllerBase
    {
        [HttpPost("events")]
        public IActionResult IngestEvents([FromBody] object payload)
        {
            return StatusCode(StatusCodes.Status501NotImplemented, new
            {
                error = "Integración biométrica no implementada aún.",
                message = "La ingesta de eventos estará disponible cuando se conecte el lector de huellas.",
                expectedSchema = new
                {
                    deviceSerial = "string",
                    events = new[]
                    {
                        new
                        {
                            externalEventId = "string",
                            externalUserId = "string",
                            eventAt = "2026-01-01T08:00:00Z",
                            eventType = "CheckIn"
                        }
                    }
                }
            });
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new
            {
                enabled = false,
                message = "Lector de huellas — próximamente",
                tablesReady = true
            });
        }
    }
}
