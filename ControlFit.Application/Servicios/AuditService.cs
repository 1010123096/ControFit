using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;

namespace ControlFit.Application.Servicios
{
    public class AuditService
    {
        private readonly IAuditLogRepository _repository;
        private readonly IUserContextService _userContext;

        public AuditService(IAuditLogRepository repository, IUserContextService userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task RegistrarAsync(string accion, string entidad, string? entidadId = null, string? detalle = null)
        {
            var log = new AuditLog
            {
                AdministradorId = _userContext.GetAdministradorId(),
                GimnasioId = _userContext.EsSuperAdmin() ? null : _userContext.GetGimnasioId(),
                Accion = accion,
                Entidad = entidad,
                EntidadId = entidadId,
                Detalle = detalle,
                FechaUtc = DateTime.UtcNow
            };

            await _repository.RegistrarAsync(log);
        }

        public async Task RegistrarLoginAsync(int administradorId, int? gimnasioId, bool exito)
        {
            var log = new AuditLog
            {
                AdministradorId = administradorId,
                GimnasioId = gimnasioId,
                Accion = exito ? "Auth.Login.Exitoso" : "Auth.Login.Fallido",
                Entidad = "Administrador",
                EntidadId = administradorId.ToString(),
                FechaUtc = DateTime.UtcNow
            };

            await _repository.RegistrarAsync(log);
        }
    }
}
