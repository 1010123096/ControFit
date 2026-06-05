using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using ControlFit.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Infrastructure.Repositorys
{
    public class MiembroRepository : IMiembroRepository
    {
        private readonly AppDbContext _context;
        public MiembroRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ActualizarAsync(Miembro miembro)
        {
            _context.Miembros.Update(miembro);
             return await _context.SaveChangesAsync() > 0;
           

        }

        public async Task<Miembro> CrearAsync(Miembro miembro)
        {
           await _context.AddAsync(miembro);
            await _context.SaveChangesAsync();
            return miembro;
            
        }

        public async Task EliminarAsync(int id)
        {
            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro != null)
            {
                _context.Miembros.Remove(miembro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Miembro>> ListarTodos()
        {
          
            return await _context.Miembros.AsNoTracking().ToListAsync();
        
            
        }

        public async Task<List<Miembro>> ListarTodosPorGimnasio(int gimnasioId)
        {
            return await _context.Miembros
                .AsNoTracking()
                .Where(x => x.GimnasioId == gimnasioId)
                .ToListAsync();
        }

        public async Task<Miembro?> ObtenerPorIdAsync(int id)
        {
            return await _context.Miembros.FindAsync(id);
        }

        public async Task<Miembro?> ObtenerPorIdValidandoGimnasio(int id, int gimnasioId)
        {
            return await _context.Miembros
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.GimnasioId == gimnasioId);
        }

    }
}
