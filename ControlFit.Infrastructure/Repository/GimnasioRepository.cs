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
    public class GimnasioRepository : IGimnasioRepository
    {
        private readonly AppDbContext _context;

        public GimnasioRepository(AppDbContext context)
        {
            _context = context;
        }

        public  async Task<bool> ActualizarAsync(Gimnasio gimnasio)
        {
            _context.gimnasios.Update(gimnasio);
           return  await _context.SaveChangesAsync() > 0;
        }

        public async Task<Gimnasio> CrearAsync(Gimnasio gimnasio)
        {
            _context.gimnasios.Add(gimnasio);
            await _context.SaveChangesAsync();
            return gimnasio;
        }

        public async  Task EliminarAsync(int id)
        {
            var gimnasio = await _context.gimnasios.FindAsync(id);

            if (gimnasio != null)
            {
                _context.gimnasios.Remove(gimnasio);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Gimnasio?>> ListarTodos()
        {
            return _context.gimnasios.AsNoTracking().ToList();
        }

                public async Task<Gimnasio?> ObtenerPorIdAsync(int id)
        {
                    return await _context.gimnasios.FindAsync(id);
        }

        public async Task<bool?> ObtenerPorNombreAsync(string nombre, int idActual)
        {
            return await _context.gimnasios
         .AnyAsync(g => g.Nombre.ToLower() == nombre.Trim().ToLower() && g.Id != idActual);
        }

        public async Task<bool?> ObtenerPorNombreAsync(string nombre)
        {
            return await _context.gimnasios
         .AnyAsync(g => g.Nombre.ToLower() == nombre.Trim().ToLower());
        }
    }
}
