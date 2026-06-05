using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Application.Repository

{
    public interface ITokenService
    {
        /// <summary>
        /// Genera un token JWT con información del administrador.
        /// </summary>
        /// <param name="administradorId">ID del administrador</param>
        /// <param name="correo">Email del administrador</param>
        /// <param name="gimnasioId">
        /// ID del gimnasio asignado al admin.
        /// null = Super Admin (acceso a todos los gimnasios)
        /// > 0 = Admin de Gimnasio (acceso solo a su gimnasio)
        /// </param>
        /// <returns>Token JWT firmado</returns>
        string GenerarToken(int administradorId, string correo, int? gimnasioId = null);
    }
}
