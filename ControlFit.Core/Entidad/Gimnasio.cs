using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Entidad
{
    public class Gimnasio
    {
        

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public DateOnly FechaCreacion { get; set; }

        public string Direccion { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public bool Estado { get; set; }
        public List<Miembro> Miembros { get; set; } = new List<Miembro>();

        public Gimnasio(string nombre, string direccion, bool estado = true, string telefono = "")
        {

            Nombre = nombre;
            FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            Direccion = direccion;
            Telefono = telefono;
            Estado = estado;
        }
        public Gimnasio() { }

        public void Actualizar(string nombre, string direccion, string telefono = "")
        {

            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }

        public void EstablecerEstado(bool estado)
        {
            Estado = estado;
        }
    }
}
