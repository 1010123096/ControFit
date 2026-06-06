using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControlFit.Application.DTO
{
    public class GimnasioCrearDTO
    {
        public string Nombre { get; set; }

        [JsonIgnore]
        public DateOnly FechaCreacion { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public bool Estado { get; set; }

        public GimnasioCrearDTO(string nombre, string direccion, string telefono = "")
        {
            Nombre = nombre;
            FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            Direccion = direccion;
            Telefono = telefono;
            Estado = true;
        }
    }
    public class GimnasioActualizarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public bool Estado { get; set; }

       
    }
}
