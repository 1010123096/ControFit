using ControlFit.Domain.Entidad;
using ControlFit.Infrastructure.Persistencia;
using ControlFit.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Infrastructure
{
    public class GimnasioRepositoryTests
    {
        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CrearAsync_ReturnsGimnasioWithId()
        {
            using var ctx = CreateDbContext();
            var repo = new GimnasioRepository(ctx);
            var gym = new Gimnasio("FitZone", "Av. Principal", true);

            var result = await repo.CrearAsync(gym);

            Assert.True(result.Id > 0);
            Assert.Equal("FitZone", result.Nombre);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_Existing_ReturnsGimnasio()
        {
            using var ctx = CreateDbContext();
            var gym = new Gimnasio("Test Gym", "Dir", true);
            ctx.gimnasios.Add(gym);
            await ctx.SaveChangesAsync();
            var repo = new GimnasioRepository(ctx);

            var result = await repo.ObtenerPorIdAsync(gym.Id);

            Assert.NotNull(result);
            Assert.Equal("Test Gym", result.Nombre);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_NonExisting_ReturnsNull()
        {
            using var ctx = CreateDbContext();
            var repo = new GimnasioRepository(ctx);

            var result = await repo.ObtenerPorIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task ActualizarAsync_Existing_ReturnsTrue()
        {
            using var ctx = CreateDbContext();
            var gym = new Gimnasio("Original", "Dir1", true);
            ctx.gimnasios.Add(gym);
            await ctx.SaveChangesAsync();

            var repo = new GimnasioRepository(ctx);
            gym.GetType().GetProperty("Nombre")!.SetValue(gym, "Actualizado");

            var result = await repo.ActualizarAsync(gym);

            Assert.True(result);
        }

        [Fact]
        public async Task EliminarAsync_Existing_RemovesGimnasio()
        {
            using var ctx = CreateDbContext();
            var gym = new Gimnasio("To Delete", "Dir", true);
            ctx.gimnasios.Add(gym);
            await ctx.SaveChangesAsync();
            var repo = new GimnasioRepository(ctx);

            await repo.EliminarAsync(gym.Id);

            Assert.Equal(0, await ctx.gimnasios.CountAsync());
        }

        [Fact]
        public async Task EliminarAsync_NonExisting_DoesNotThrow()
        {
            using var ctx = CreateDbContext();
            var repo = new GimnasioRepository(ctx);

            await repo.EliminarAsync(999);

            Assert.Equal(0, await ctx.gimnasios.CountAsync());
        }

        [Fact]
        public async Task ListarTodos_ReturnsAll()
        {
            using var ctx = CreateDbContext();
            ctx.gimnasios.AddRange(
                new Gimnasio("G1", "D1", true),
                new Gimnasio("G2", "D2", true));
            await ctx.SaveChangesAsync();
            var repo = new GimnasioRepository(ctx);

            var result = await repo.ListarTodos();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ObtenerPorNombreAsync_Existing_ReturnsTrue()
        {
            using var ctx = CreateDbContext();
            ctx.gimnasios.Add(new Gimnasio("UNIQUE NAME", "Dir", true));
            await ctx.SaveChangesAsync();
            var repo = new GimnasioRepository(ctx);

            var result = await repo.ObtenerPorNombreAsync("unique name");

            Assert.True(result);
        }

        [Fact]
        public async Task ObtenerPorNombreAsync_NonExisting_ReturnsFalse()
        {
            using var ctx = CreateDbContext();
            var repo = new GimnasioRepository(ctx);

            var result = await repo.ObtenerPorNombreAsync("No Existe");

            Assert.False(result);
        }

        [Fact]
        public async Task ObtenerPorNombreAsync_WithIdActual_ExcludesSelf()
        {
            using var ctx = CreateDbContext();
            var gym = new Gimnasio("MiGym", "Dir", true);
            ctx.gimnasios.Add(gym);
            await ctx.SaveChangesAsync();
            var repo = new GimnasioRepository(ctx);

            var result = await repo.ObtenerPorNombreAsync("MiGym", gym.Id);

            Assert.False(result);
        }
    }
}
