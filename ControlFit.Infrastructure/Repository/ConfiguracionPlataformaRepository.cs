using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using ControlFit.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Infrastructure.Repository
{
    public class ConfiguracionPlataformaRepository : IConfiguracionPlataformaRepository
    {
        private readonly AppDbContext _context;

        public ConfiguracionPlataformaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ConfiguracionPlataforma?> ObtenerAsync()
        {
            return await _context.ConfiguracionesPlataforma.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<ConfiguracionPlataforma> ObtenerOCrearPorDefectoAsync()
        {
            var existing = await _context.ConfiguracionesPlataforma.FirstOrDefaultAsync();
            if (existing != null)
                return existing;

            var defaults = new ConfiguracionPlataforma("CossGym", "contacto@cossgym.com", "+51 999 999 999");
            _context.ConfiguracionesPlataforma.Add(defaults);
            await _context.SaveChangesAsync();
            return defaults;
        }

        public async Task<ConfiguracionPlataforma> GuardarAsync(ConfiguracionPlataforma configuracion)
        {
            _context.ConfiguracionesPlataforma.Update(configuracion);
            await _context.SaveChangesAsync();
            return configuracion;
        }
    }
}
