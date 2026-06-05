using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Entidad
{
    public class Miembro
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Correo { get; private set; }
        public string Telefono { get; private set; }
        public DateOnly FechaNacimiento { get; private set; }
        public bool Estado { get; private set; }
        public int GimnasioId { get; private set; }
        public Gimnasio Gimnasio { get; private set; }


        public Miembro(string nombre, string correo, string telefono, DateOnly fechaNacimiento, int gimnasioId)
        {

            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            Estado = true;
            GimnasioId = gimnasioId;
           
        }

    
        public Miembro()
        {
        }

      
        
        public void Actualizar(String nombre, String correo, string telefono)
        {
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
        }
    }
}
