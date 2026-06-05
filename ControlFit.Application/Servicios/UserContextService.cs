using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ControlFit.Application.Servicios
{
    /// <summary>
    /// Implementación de IUserContextService.
    /// Extrae información del usuario autenticado desde los claims del JWT.
    /// </summary>
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Extrae el claim "GimnasioId" del JWT.
        /// </summary>
        public int GetGimnasioId()
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("GimnasioId");
            if (string.IsNullOrEmpty(claim?.Value))
                return 0;

            return int.TryParse(claim.Value, out var value) ? value : 0;
        }

        /// <summary>
        /// Extrae el claim "email" del JWT.
        /// </summary>
        public string GetUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value ?? string.Empty;
        }

        /// <summary>
        /// Extrae el claim "sub" (subject/administradorId) del JWT.
        /// </summary>
        public int GetAdministradorId()
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub");
            if (string.IsNullOrEmpty(claim?.Value))
                return 0;

            return int.TryParse(claim.Value, out var value) ? value : 0;
        }

        /// <summary>
        /// Verifica si el usuario es Super Admin.
        /// Un Super Admin tiene GimnasioId = 0 o no tiene el claim.
        /// </summary>
        public bool EsSuperAdmin()
        {
            return GetGimnasioId() == 0;
        }

        /// <summary>
        /// Verifica si el usuario es Admin de Gimnasio.
        /// Un Admin de Gimnasio tiene GimnasioId > 0.
        /// </summary>
        public bool EsAdminGimnasio()
        {
            return GetGimnasioId() > 0;
        }
    }
}
