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
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public int? GimnasioId { get; set; }
        public Gimnasio? Gimnasio { get; set; }

        public Administrador(string nombreCompleto, string correo, string contrasena, int? gimnasioId = null)
        {
            NombreCompleto = nombreCompleto;
            Correo = correo;
            Contrasena = contrasena;
            GimnasioId = gimnasioId;
        }

        public Administrador()
        {
        }

        public bool ValidarPassword(string passwordPlano)
           => BCrypt.Net.BCrypt.Verify(passwordPlano, Contrasena);
       
        public void AsignarContrasena(string passwordPlano)
        {
            Contrasena = BCrypt.Net.BCrypt.HashPassword(passwordPlano);
        }
    }
}
