using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Entidad
{
    public class Administrador
    {
        public int Id { get; private set; }
        public string NombreCompleto { get; private set; }
       /* [NotMapped]
        public string Documento { get; private set; }*/
        public string Correo { get; private set; }
       /* [NotMapped]
        public string Telefono { get; private set; }
        [NotMapped]
        public DateTime FechaCreacion { get; private set; }
        [NotMapped]
        public string Direccion { get; private set; }
        [NotMapped]
        public bool Estado { get; private set; }*/
        public string Contrasena { get; private set; }

        /// <summary>
        /// ID del gimnasio asignado al administrador.
        /// null = Super Admin (acceso a todos los gimnasios)
        /// > 0 = Admin de Gimnasio (acceso solo a su gimnasio)
        /// </summary>
        public int? GimnasioId { get; private set; }
        public Gimnasio Gimnasio { get; private set; }

        public Administrador(string nombreCompleto, string correo, string contrasena, int? gimnasioId = null)
        {
            NombreCompleto = nombreCompleto;
            Correo = correo;
            Contrasena = contrasena;
            GimnasioId = gimnasioId;
           // FechaCreacion = DateTime.UtcNow;
           // Estado = true;
        }

        public Administrador()
        {
        }

        //  public void Activar() => Estado = true;

        // public void Desactivar() => Estado = false;

        public bool ValidarPassword(string passwordPlano)
           => BCrypt.Net.BCrypt.Verify(passwordPlano, Contrasena);
       
        public void AsignarContrasena(string passwordPlano)
        {
            Contrasena = BCrypt.Net.BCrypt.HashPassword(passwordPlano);
        }
    }
}
