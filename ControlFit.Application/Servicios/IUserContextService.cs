using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Application.Servicios
{
    /// <summary>
    /// Servicio para extraer información del usuario autenticado del contexto HTTP.
    /// Implementa seguridad multi-tenant leyendo claims del JWT.
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Obtiene el ID del gimnasio del usuario autenticado.
        /// </summary>
        /// <returns>
        /// ID del gimnasio si es Admin de Gimnasio (GimnasioId > 0).
        /// 0 si es Super Admin (no tiene gimnasio asignado).
        /// </returns>
        int GetGimnasioId();

        /// <summary>
        /// Obtiene el correo del usuario autenticado.
        /// </summary>
        /// <returns>String vacío si no hay usuario autenticado.</returns>
        string GetUserEmail();

        /// <summary>
        /// Obtiene el ID del administrador autenticado.
        /// </summary>
        /// <returns>ID del administrador, 0 si no hay usuario autenticado.</returns>
        int GetAdministradorId();

        /// <summary>
        /// Determina si el usuario es Super Admin.
        /// </summary>
        /// <returns>true si es Super Admin (GimnasioId = 0), false si es Admin de Gimnasio.</returns>
        bool EsSuperAdmin();

        /// <summary>
        /// Determina si el usuario es Admin de Gimnasio.
        /// </summary>
        /// <returns>true si es Admin de Gimnasio (GimnasioId > 0), false si es Super Admin.</returns>
        bool EsAdminGimnasio();
    }
}
