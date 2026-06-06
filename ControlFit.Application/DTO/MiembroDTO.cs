using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ControlFit.Application.DTO
{
    public class MiembroDTO
    {
        public int Id { get; set; }

        [JsonPropertyName("nombreCompleto")]
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateOnly? FechaNacimiento { get; set; }
        public int GimnasioId { get; set; }
        public string? GimnasioNombre { get; set; }
    }
}
