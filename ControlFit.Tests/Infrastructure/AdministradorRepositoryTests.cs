using ControlFit.Domain.Entidad;
using ControlFit.Infrastructure.Persistencia;
using ControlFit.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Infrastructure
{
    public class AdministradorRepositoryTests
    {
        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GuardarAsync_AddsAdmin()
        {
            using var ctx = CreateDbContext();
            var repo = new AdministradorRepository(ctx);
            var admin = new Administrador("Test Admin", "test@test.com", "hash123");

            await repo.GuardarAsync(admin);

            Assert.Equal(1, await ctx.Administradores.CountAsync());
        }

        [Fact]
        public async Task ObtenerPorCorreoAsync_Existing_ReturnsAdmin()
        {
            using var ctx = CreateDbContext();
            ctx.Administradores.Add(new Administrador("Existing", "exist@test.com", "hash"));
            await ctx.SaveChangesAsync();
            var repo = new AdministradorRepository(ctx);

            var result = await repo.ObtenerPorCorreoAsync("exist@test.com");

            Assert.NotNull(result);
            Assert.Equal("Existing", result.NombreCompleto);
        }

        [Fact]
        public async Task ObtenerPorCorreoAsync_NonExisting_ReturnsNull()
        {
            using var ctx = CreateDbContext();
            var repo = new AdministradorRepository(ctx);

            var result = await repo.ObtenerPorCorreoAsync("nonexist@test.com");

            Assert.Null(result);
        }
    }
}
