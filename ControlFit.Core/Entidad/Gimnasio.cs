using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Entidad
{
    public class Gimnasio
    {
        

        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public DateOnly FechaCreacion { get; private set; }

        public string Direccion { get; private set; }

        public bool Estado { get; private set; }
        public List<Miembro> Miembros { get; private set; }

        public Gimnasio(string nombre, string direccion, bool estado)
        {

            Nombre = nombre;
            FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            Direccion = direccion;
            Estado = estado;
        }
        public Gimnasio() { }

        public void Actualizar(string nombre, string direccion)
        {

            Nombre = nombre;
            Direccion = direccion;
        }

        public void EstablecerEstado(bool estado)
        {
            Estado = estado;
        }
    }
}
