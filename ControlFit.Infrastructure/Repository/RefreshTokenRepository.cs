using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using ControlFit.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearAsync(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> ObtenerPorHashAsync(string tokenHash)
        {
            return await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TokenHash == tokenHash);
        }

        public async Task RevocarAsync(RefreshToken token, string? replacedByHash = null)
        {
            var entity = await _context.RefreshTokens.FindAsync(token.Id);
            if (entity == null) return;

            entity.RevokedAt = DateTime.UtcNow;
            entity.ReplacedByTokenHash = replacedByHash;
            await _context.SaveChangesAsync();
        }

        public async Task RevocarTodosDelAdministradorAsync(int administradorId)
        {
            var tokens = await _context.RefreshTokens
                .Where(t => t.AdministradorId == administradorId && t.RevokedAt == null)
                .ToListAsync();

            foreach (var token in tokens)
                token.RevokedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
