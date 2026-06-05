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
    public class MembresiaRepository : IMembresiaRepository
    {
        private readonly AppDbContext _appDbContext;

        public MembresiaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> ActualizarAsync(Membresia membresia)
        {
            var membresiaExistente = await _appDbContext.Membresias
        .FirstOrDefaultAsync(m => m.Id == membresia.Id);
            if (membresiaExistente == null)
            {
                throw new Exception("La membresía no se encuentra registrada");
            }
            _appDbContext.Entry(membresiaExistente).CurrentValues.SetValues(membresia);
            return await _appDbContext.SaveChangesAsync() > 0;
             
        }

        public async Task<Membresia> CrearAsync(Membresia membresia)
        {
            var membresiaExistente = await _appDbContext.Membresias
        .FirstOrDefaultAsync(m => m.Nombre == membresia.Nombre);
            if (membresiaExistente != null)
            {
                throw new Exception("La membresía ya se encuentra registrada");
            }
            await _appDbContext.Membresias.AddAsync(membresia);
            await _appDbContext.SaveChangesAsync();
            return membresia;
        }


        public async Task EliminarAsync(int id)
        {
            var membresia = await _appDbContext.Membresias.FindAsync(id);
            if (membresia != null)
            {
                _appDbContext.Membresias.Remove(membresia);
               await  _appDbContext.SaveChangesAsync();
            }
            
        }

        public async Task<List<Membresia>> ListarTodos()
        {

            return await _appDbContext.Membresias.AsNoTracking().ToListAsync();
        }

        public async Task<List<Membresia>> ListarTodosPorGimnasio(int gimnasioId)
        {
            return await _appDbContext.Membresias
                .AsNoTracking()
                .Where(x => x.GimnasioId == gimnasioId)
                .ToListAsync();
        }

        public async Task<Membresia?> ObtenerPorIdAsync(int id)
        {

            return await _appDbContext.Membresias.FindAsync(id);
        }

        public async Task<Membresia?> ObtenerPorIdValidandoGimnasio(int id, int gimnasioId)
        {
            return await _appDbContext.Membresias
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.GimnasioId == gimnasioId);
        }
    }
}
