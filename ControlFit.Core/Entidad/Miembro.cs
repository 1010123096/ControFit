using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Entidad
{
    public class Miembro
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateOnly FechaNacimiento { get; set; }
        public bool Estado { get; set; }
        public int GimnasioId { get; set; }
        public Gimnasio? Gimnasio { get; set; }


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
