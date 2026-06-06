using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Infrastructure.Persistencia;
using ControlFit.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Infrastructure
{
    public class MembresiaRepositoryTests
    {
        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        private static Membresia CreateMembresia(string nombre, double precio, bool estado, int gimnasioId)
        {
            return new Membresia(nombre, 30, precio, 0, 0, 0, estado, gimnasioId);
        }

        [Fact]
        public async Task CrearAsync_NewMembresia_ReturnsWithId()
        {
            using var ctx = CreateDbContext();
            var repo = new MembresiaRepository(ctx);
            var membresia = CreateMembresia("Premium", 100.0, true, 1);

            var result = await repo.CrearAsync(membresia);

            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task CrearAsync_DuplicateName_Throws()
        {
            using var ctx = CreateDbContext();
            ctx.Membresias.Add(CreateMembresia("Basica", 50.0, true, 1));
            await ctx.SaveChangesAsync();
            var repo = new MembresiaRepository(ctx);

            var ex = await Assert.ThrowsAsync<DomainException>(() =>
                repo.CrearAsync(CreateMembresia("Basica", 60.0, true, 1)));
            Assert.Contains("ya se encuentra registrada", ex.Message);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_Existing_ReturnsMembresia()
        {
            using var ctx = CreateDbContext();
            var m = CreateMembresia("Test", 50.0, true, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();
            var repo = new MembresiaRepository(ctx);

            var result = await repo.ObtenerPorIdAsync(m.Id);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Nombre);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_NonExisting_ReturnsNull()
        {
            using var ctx = CreateDbContext();
            var repo = new MembresiaRepository(ctx);

            var result = await repo.ObtenerPorIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task ActualizarAsync_Existing_ReturnsTrue()
        {
            using var ctx = CreateDbContext();
            var m = CreateMembresia("Original", 50.0, true, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();

            var repo = new MembresiaRepository(ctx);
            m.GetType().GetProperty("Precio")!.SetValue(m, 80.0);

            var result = await repo.ActualizarAsync(m);

            Assert.True(result);
        }

        [Fact]
        public async Task ActualizarAsync_NonExistingName_Throws()
        {
            using var ctx = CreateDbContext();
            var repo = new MembresiaRepository(ctx);

            var ex = await Assert.ThrowsAsync<DomainException>(() =>
                repo.ActualizarAsync(CreateMembresia("NoExiste", 50.0, true, 1)));
            Assert.Contains("no se encuentra registrada", ex.Message);
        }

        [Fact]
        public async Task ListarTodos_ReturnsAll()
        {
            using var ctx = CreateDbContext();
            ctx.Membresias.AddRange(
                CreateMembresia("A", 10.0, true, 1),
                CreateMembresia("B", 20.0, true, 1));
            await ctx.SaveChangesAsync();
            var repo = new MembresiaRepository(ctx);

            var result = await repo.ListarTodos();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ListarTodosPorGimnasio_FiltersByGym()
        {
            using var ctx = CreateDbContext();
            ctx.Membresias.AddRange(
                CreateMembresia("Gym1", 10.0, true, 1),
                CreateMembresia("Gym2", 20.0, true, 2));
            await ctx.SaveChangesAsync();
            var repo = new MembresiaRepository(ctx);

            var result = await repo.ListarTodosPorGimnasio(1);

            Assert.Single(result);
        }

        [Fact]
        public async Task ObtenerPorIdValidandoGimnasio_WrongGym_ReturnsNull()
        {
            using var ctx = CreateDbContext();
            var m = CreateMembresia("Test", 50.0, true, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();
            var repo = new MembresiaRepository(ctx);

            var result = await repo.ObtenerPorIdValidandoGimnasio(m.Id, 2);

            Assert.Null(result);
        }
    }
}
