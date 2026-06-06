using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Application.DTO
{
    /// <summary>
    /// DTO para registrar un nuevo administrador.
    /// </summary>
    public class RegistroAdministradorDTO       
    {
        public string nombreCompleto { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string contrasena { get; set; } = string.Empty;
        
        /// <summary>
        /// ID del gimnasio asignado al administrador.
        /// null = Super Admin (acceso a todos los gimnasios)
        /// > 0 = Admin de Gimnasio (acceso solo a su gimnasio)
        /// </summary>
        public int? GimnasioId { get; set; }
    }

    /// <summary>
    /// DTO para autenticar administrador (login).
    /// </summary>
    public class LoginAdministradorDTO
    {
        public string correo { get; set; } = string.Empty;
        public string contrasena { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para lectura de datos de administrador.
    /// </summary>
    public class AdministradorDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public int? GimnasioId { get; set; }
        public string GimnasioNombre { get; set; } = string.Empty;
    }
}
