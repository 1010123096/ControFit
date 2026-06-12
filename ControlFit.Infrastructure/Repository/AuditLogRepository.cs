using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using ControlFit.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Infrastructure.Repository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarAsync(AuditLog log)
        {
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<AuditLogListado> Items, int Total)> ConsultarAsync(
            int? gimnasioId,
            string? accion,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int page,
            int pageSize)
        {
            var query = _context.AuditLogs.AsNoTracking().AsQueryable();

            if (gimnasioId.HasValue)
                query = query.Where(a => a.GimnasioId == gimnasioId.Value);

            if (!string.IsNullOrWhiteSpace(accion))
            {
                var term = accion.Trim();
                query = query.Where(a => a.Accion.Contains(term) || a.Entidad.Contains(term));
            }

            if (fechaInicio.HasValue)
            {
                var inicio = DateTime.SpecifyKind(fechaInicio.Value.Date, DateTimeKind.Utc);
                query = query.Where(a => a.FechaUtc >= inicio);
            }

            if (fechaFin.HasValue)
            {
                var fin = DateTime.SpecifyKind(fechaFin.Value.Date.AddDays(1), DateTimeKind.Utc);
                query = query.Where(a => a.FechaUtc < fin);
            }

            var total = await query.CountAsync();

            var items = await (
                from log in query
                join admin in _context.Administradores on log.AdministradorId equals admin.Id into admins
                from admin in admins.DefaultIfEmpty()
                join gym in _context.gimnasios on log.GimnasioId equals gym.Id into gyms
                from gym in gyms.DefaultIfEmpty()
                orderby log.FechaUtc descending
                select new AuditLogListado
                {
                    Id = log.Id,
                    AdministradorId = log.AdministradorId,
                    AdministradorCorreo = admin != null ? admin.Correo : null,
                    GimnasioId = log.GimnasioId,
                    GimnasioNombre = gym != null ? gym.Nombre : null,
                    Accion = log.Accion,
                    Entidad = log.Entidad,
                    EntidadId = log.EntidadId,
                    Detalle = log.Detalle,
                    FechaUtc = log.FechaUtc
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }
    }
}
