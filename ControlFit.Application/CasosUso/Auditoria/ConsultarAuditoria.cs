using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;

namespace ControlFit.Application.CasosUso.Auditoria
{
    public class ConsultarAuditoria
    {
        private readonly IAuditLogRepository _repository;
        private readonly IUserContextService _userContext;

        public ConsultarAuditoria(IAuditLogRepository repository, IUserContextService userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<AuditLogQueryResultDTO> EjecutarAsync(
            int? gimnasioId,
            string? accion,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int page,
            int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 25;
            if (pageSize > 100) pageSize = 100;

            int? filtroGimnasio = gimnasioId;

            if (!_userContext.EsSuperAdmin())
                throw new DomainException("Solo el Super Administrador puede consultar la auditoría.");

            var (items, total) = await _repository.ConsultarAsync(
                filtroGimnasio,
                accion,
                fechaInicio,
                fechaFin,
                page,
                pageSize);

            return new AuditLogQueryResultDTO
            {
                Items = items.Select(Map).ToList(),
                Total = total,
                Page = page,
                PageSize = pageSize
            };
        }

        private static AuditLogListadoDTO Map(AuditLogListado item) => new()
        {
            Id = item.Id,
            AdministradorId = item.AdministradorId,
            AdministradorCorreo = item.AdministradorCorreo,
            GimnasioId = item.GimnasioId,
            GimnasioNombre = item.GimnasioNombre,
            Accion = item.Accion,
            Entidad = item.Entidad,
            EntidadId = item.EntidadId,
            Detalle = item.Detalle,
            FechaUtc = item.FechaUtc
        };
    }
}
