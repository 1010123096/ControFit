using ControlFit.Domain.Entidad;
using ControlFit.Infrastructure.Persistencia;
using ControlFit.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Infrastructure
{
    public class AsignacionMembresiaRepositoryTests
    {
        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        private static Membresia CreateMembresia(int id, int gimnasioId)
        {
            var m = new Membresia($"Memb-{id}", 30, 50.0, 0, 0, 0, true, gimnasioId);
            typeof(Membresia).GetProperty("Id")!.SetValue(m, id);
            return m;
        }

        private static AsignacionMembresia CreateAsignacion(int miembroId, Membresia membresia)
        {
            return new AsignacionMembresia(miembroId, membresia);
        }

        [Fact]
        public async Task CrearAsync_ReturnsAsignacionWithId()
        {
            using var ctx = CreateDbContext();
            var membresia = CreateMembresia(1, 1);
            ctx.Membresias.Add(membresia);
            await ctx.SaveChangesAsync();

            var repo = new AsignacionMembresiaRepository(ctx);
            var asignacion = CreateAsignacion(1, membresia);

            var result = await repo.CrearAsync(asignacion);

            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_Existing_ReturnsAsignacion()
        {
            using var ctx = CreateDbContext();
            var m = CreateMembresia(1, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();

            var a = CreateAsignacion(1, m);
            ctx.AsignacionesMembresia.Add(a);
            await ctx.SaveChangesAsync();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.ObtenerPorIdAsync(a.Id);

            Assert.NotNull(result);
            Assert.Equal(1, result.MiembroId);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_NonExisting_ReturnsNull()
        {
            using var ctx = CreateDbContext();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.ObtenerPorIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task ObtenerTodosAsync_ReturnsAll()
        {
            using var ctx = CreateDbContext();
            var m = CreateMembresia(1, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();

            ctx.AsignacionesMembresia.AddRange(
                CreateAsignacion(1, m),
                CreateAsignacion(2, m));
            await ctx.SaveChangesAsync();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.ObtenerTodosAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ObtenerTodosPorGimnasio_FiltersByGymViaMembresias()
        {
            using var ctx = CreateDbContext();
            var m1 = CreateMembresia(1, 1);
            var m2 = CreateMembresia(2, 2);
            ctx.Membresias.AddRange(m1, m2);
            await ctx.SaveChangesAsync();

            ctx.AsignacionesMembresia.AddRange(
                CreateAsignacion(1, m1),
                CreateAsignacion(2, m2));
            await ctx.SaveChangesAsync();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.ObtenerTodosPorGimnasio(1);

            Assert.Single(result);
        }

        [Fact]
        public async Task ObtenerPorMiembroGimnasio_FiltersCorrectly()
        {
            using var ctx = CreateDbContext();
            var m = CreateMembresia(1, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();

            ctx.AsignacionesMembresia.AddRange(
                CreateAsignacion(1, m),
                CreateAsignacion(2, m));
            await ctx.SaveChangesAsync();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.ObtenerPorMiembroGimnasio(1, 1);

            Assert.Single(result);
        }

        [Fact]
        public async Task EliminarAsync_Existing_ReturnsTrue()
        {
            using var ctx = CreateDbContext();
            var m = CreateMembresia(1, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();

            var a = CreateAsignacion(1, m);
            ctx.AsignacionesMembresia.Add(a);
            await ctx.SaveChangesAsync();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.EliminarAsync(a.Id);

            Assert.True(result);
            Assert.Equal(0, await ctx.AsignacionesMembresia.CountAsync());
        }

        [Fact]
        public async Task EliminarAsync_NonExisting_ReturnsFalse()
        {
            using var ctx = CreateDbContext();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.EliminarAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task ObtenerActivaAsync_ActiveExists_ReturnsIt()
        {
            using var ctx = CreateDbContext();
            var m1 = CreateMembresia(1, 1);
            var m2 = CreateMembresia(2, 1);
            ctx.Membresias.AddRange(m1, m2);
            await ctx.SaveChangesAsync();

            var expired = CreateAsignacion(1, m1);
            typeof(AsignacionMembresia).GetProperty("FechaInicio")!.SetValue(expired, DateTime.Today.AddDays(-60));
            typeof(AsignacionMembresia).GetProperty("FechaFin")!.SetValue(expired, DateTime.Today.AddDays(-30));

            var active = CreateAsignacion(1, m2);
            typeof(AsignacionMembresia).GetProperty("FechaInicio")!.SetValue(active, DateTime.Today.AddDays(-10));
            typeof(AsignacionMembresia).GetProperty("FechaFin")!.SetValue(active, DateTime.Today.AddDays(20));

            ctx.AsignacionesMembresia.AddRange(expired, active);
            await ctx.SaveChangesAsync();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.ObtenerActivaAsync(1);

            Assert.NotNull(result);
            Assert.Equal(m2.Id, result.MembresiaId);
        }

        [Fact]
        public async Task ObtenerActivaAsync_NoneActive_ReturnsNull()
        {
            using var ctx = CreateDbContext();
            var m = CreateMembresia(1, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();

            var expired = CreateAsignacion(1, m);
            typeof(AsignacionMembresia).GetProperty("FechaInicio")!.SetValue(expired, DateTime.Today.AddDays(-60));
            typeof(AsignacionMembresia).GetProperty("FechaFin")!.SetValue(expired, DateTime.Today.AddDays(-1));
            ctx.AsignacionesMembresia.Add(expired);
            await ctx.SaveChangesAsync();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.ObtenerActivaAsync(1);

            Assert.Null(result);
        }
    }
}
