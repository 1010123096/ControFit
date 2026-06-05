using ControlFit.Domain.Entidad;
using ControlFit.Infrastructure.Persistencia;
using ControlFit.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Infrastructure
{
    public class MiembroRepositoryTests
    {
        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CrearAsync_ReturnsMiembroWithId()
        {
            using var ctx = CreateDbContext();
            var repo = new MiembroRepository(ctx);
            var miembro = new Miembro("Juan Perez", "juan@test.com", "999888777", new DateOnly(1990, 5, 10), 1);

            var result = await repo.CrearAsync(miembro);

            Assert.True(result.Id > 0);
            Assert.Equal("Juan Perez", result.Nombre);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_Existing_ReturnsMiembro()
        {
            using var ctx = CreateDbContext();
            var miembro = new Miembro("Ana", "ana@test.com", "111", new DateOnly(1995, 1, 1), 1);
            ctx.Miembros.Add(miembro);
            await ctx.SaveChangesAsync();
            var repo = new MiembroRepository(ctx);

            var result = await repo.ObtenerPorIdAsync(miembro.Id);

            Assert.NotNull(result);
            Assert.Equal("Ana", result.Nombre);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_NonExisting_ReturnsNull()
        {
            using var ctx = CreateDbContext();
            var repo = new MiembroRepository(ctx);

            var result = await repo.ObtenerPorIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task ActualizarAsync_Existing_ReturnsTrue()
        {
            using var ctx = CreateDbContext();
            var miembro = new Miembro("Original", "o@test.com", "222", new DateOnly(2000, 1, 1), 1);
            ctx.Miembros.Add(miembro);
            await ctx.SaveChangesAsync();

            var repo = new MiembroRepository(ctx);
            miembro.GetType().GetProperty("Nombre")!.SetValue(miembro, "Original Modificado");

            var result = await repo.ActualizarAsync(miembro);

            Assert.True(result);
        }

        [Fact]
        public async Task EliminarAsync_Existing_RemovesMiembro()
        {
            using var ctx = CreateDbContext();
            var miembro = new Miembro("Borrar", "b@test.com", "333", new DateOnly(2000, 1, 1), 1);
            ctx.Miembros.Add(miembro);
            await ctx.SaveChangesAsync();
            var repo = new MiembroRepository(ctx);

            await repo.EliminarAsync(miembro.Id);

            Assert.Equal(0, await ctx.Miembros.CountAsync());
        }

        [Fact]
        public async Task EliminarAsync_NonExisting_DoesNotThrow()
        {
            using var ctx = CreateDbContext();
            var repo = new MiembroRepository(ctx);

            await repo.EliminarAsync(999);

            Assert.Equal(0, await ctx.Miembros.CountAsync());
        }

        [Fact]
        public async Task ListarTodos_ReturnsAll()
        {
            using var ctx = CreateDbContext();
            ctx.Miembros.AddRange(
                new Miembro("A", "a@t.com", "1", new DateOnly(2000, 1, 1), 1),
                new Miembro("B", "b@t.com", "2", new DateOnly(2000, 1, 1), 1));
            await ctx.SaveChangesAsync();
            var repo = new MiembroRepository(ctx);

            var result = await repo.ListarTodos();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ListarTodosPorGimnasio_FiltersByGym()
        {
            using var ctx = CreateDbContext();
            ctx.Miembros.AddRange(
                new Miembro("Gym1", "g1@t.com", "1", new DateOnly(2000, 1, 1), 1),
                new Miembro("Gym2", "g2@t.com", "2", new DateOnly(2000, 1, 1), 2));
            await ctx.SaveChangesAsync();
            var repo = new MiembroRepository(ctx);

            var result = await repo.ListarTodosPorGimnasio(1);

            Assert.Single(result);
            Assert.Equal("Gym1", result[0].Nombre);
        }

        [Fact]
        public async Task ObtenerPorIdValidandoGimnasio_Valid_ReturnsMiembro()
        {
            using var ctx = CreateDbContext();
            var miembro = new Miembro("Valid", "v@t.com", "555", new DateOnly(2000, 1, 1), 3);
            ctx.Miembros.Add(miembro);
            await ctx.SaveChangesAsync();
            var repo = new MiembroRepository(ctx);

            var result = await repo.ObtenerPorIdValidandoGimnasio(miembro.Id, 3);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ObtenerPorIdValidandoGimnasio_WrongGym_ReturnsNull()
        {
            using var ctx = CreateDbContext();
            var miembro = new Miembro("Wrong Gym", "w@t.com", "666", new DateOnly(2000, 1, 1), 1);
            ctx.Miembros.Add(miembro);
            await ctx.SaveChangesAsync();
            var repo = new MiembroRepository(ctx);

            var result = await repo.ObtenerPorIdValidandoGimnasio(miembro.Id, 2);

            Assert.Null(result);
        }
    }
}
