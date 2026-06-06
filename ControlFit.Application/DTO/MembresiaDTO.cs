using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ControlFit.Application.DTO
{
    // Response DTO
    public class MembresiaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("duracionDias")]
        public int Duración { get; set; }
        public double Precio { get; set; }
        public bool Estado { get; set; }
        public int MaximoIngresosPorDia { get; set; }
        public int MaximoIngresosPorSemana { get; set; }
        public int MaximoIngresosTotales { get; set; }
        public int GimnasioId { get; set; }

        public MembresiaDTO() { }

        public MembresiaDTO(int id, string nombre, int duracion, double precio, bool estado, int maxDia, int maxSemana, int maxTotal, int gimnasioId)
        {
            Id = id;
            Nombre = nombre;
            Duración = duracion;
            Precio = precio;
            Estado = estado;
            MaximoIngresosPorDia = maxDia;
            MaximoIngresosPorSemana = maxSemana;
            MaximoIngresosTotales = maxTotal;
            GimnasioId = gimnasioId;
        }
    }

    // Request DTOs
    public class CrearMembresiaDTO
    {
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("duracionDias")]
        public int Duración { get; set; }
        public double Precio { get; set; }
        public int MaximoIngresosPorDia { get; set; }
        public int MaximoIngresosPorSemana { get; set; }
        public int MaximoIngresosTotales { get; set; }
        public int GimnasioId { get; set; }
    }

    public class ActualizarMembresiaDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        [JsonPropertyName("duracionDias")]
        public int? Duración { get; set; }
        public double? Precio { get; set; }
        public bool? Estado { get; set; }
        public int? MaximoIngresosPorDia { get; set; }
        public int? MaximoIngresosPorSemana { get; set; }
        public int? MaximoIngresosTotales { get; set; }
        public int? GimnasioId { get; set; }
    }
}
