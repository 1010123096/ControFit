using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using ControlFit.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Infrastructure.Repository
{
    public class AsignacionMembresiaRepository : IAsignacionMembresiaRepository
    {
        private readonly AppDbContext _context;

        public AsignacionMembresiaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AsignacionMembresia> CrearAsync(
            AsignacionMembresia asignacion)
        {
            _context.AsignacionesMembresia.Add(asignacion);

            await _context.SaveChangesAsync();

            return asignacion;
        }

        public async Task<AsignacionMembresia?> ObtenerPorIdAsync(int id)
        {
            return await _context.AsignacionesMembresia
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<AsignacionMembresia>> ObtenerTodosAsync()
        {
            return await _context.AsignacionesMembresia
                .ToListAsync();
        }

        public async Task<List<AsignacionMembresia>> ObtenerTodosPorGimnasio(int gimnasioId)
        {
            return await (from asignacion in _context.AsignacionesMembresia
                          join membresia in _context.Membresias on asignacion.MembresiaId equals membresia.Id
                          where membresia.GimnasioId == gimnasioId
                          select asignacion)
                .ToListAsync();
        }

        public async Task<List<AsignacionMembresia>> ObtenerPorMiembroGimnasio(int miembroId, int gimnasioId)
        {
            return await (from asignacion in _context.AsignacionesMembresia
                          join membresia in _context.Membresias on asignacion.MembresiaId equals membresia.Id
                          where asignacion.MiembroId == miembroId && membresia.GimnasioId == gimnasioId
                          select asignacion)
                .ToListAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var asignacion = await _context.AsignacionesMembresia
                .FirstOrDefaultAsync(x => x.Id == id);

            if (asignacion == null)
                return false;

            _context.AsignacionesMembresia.Remove(asignacion);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<AsignacionMembresia?> ObtenerActivaAsync(int miembroId)
        {
            return await _context.AsignacionesMembresia
                .FirstOrDefaultAsync(x =>
                    x.MiembroId == miembroId &&
                    x.Estado &&
                    x.FechaFin >= DateTime.Today);
        }
    }
}
