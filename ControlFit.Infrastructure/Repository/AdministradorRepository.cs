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
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly AppDbContext _context;

        public AdministradorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Administrador?> ObtenerPorCorreoAsync(string correo)
        {
            return await _context.Administradores.Include(a => a.Gimnasio).FirstOrDefaultAsync(a => a.Correo == correo);
        }

        public async Task GuardarAsync(Administrador admin)
        {
            _context.Administradores.Add(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Administrador>> ObtenerTodosAsync()
        {
            return await _context.Administradores.AsNoTracking().ToListAsync();
        }
    }
}
