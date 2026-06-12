using ControlFit.Domain.Entidad;

namespace ControlFit.Domain.Interfaz_puertos_
{
    public interface IAuditLogRepository
    {
        Task RegistrarAsync(AuditLog log);

        Task<(List<AuditLogListado> Items, int Total)> ConsultarAsync(
            int? gimnasioId,
            string? accion,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int page,
            int pageSize);
    }
}
