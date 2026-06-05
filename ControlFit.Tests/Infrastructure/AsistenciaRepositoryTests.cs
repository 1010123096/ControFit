using ControlFit.Domain.Entidad;
using ControlFit.Infrastructure.Persistencia;
using ControlFit.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Infrastructure
{
    public class AsistenciaRepositoryTests
    {
        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task RegistrarAsync_AddsAsistencia()
        {
            using var ctx = CreateDbContext();
            var repo = new AsistenciaRepository(ctx);
            var asistencia = new Asistencia(1, 1);

            await repo.RegistrarAsync(asistencia);

            Assert.Equal(1, await ctx.Asistencias.CountAsync());
        }

        [Fact]
        public async Task YaIngresoHoyAsync_HasEntryToday_ReturnsTrue()
        {
            using var ctx = CreateDbContext();
            var a = new Asistencia(1, 1);
            typeof(Asistencia).GetProperty("FechaHoraAcceso")!.SetValue(a, DateTime.Today.AddHours(10));
            ctx.Asistencias.Add(a);
            await ctx.SaveChangesAsync();
            var repo = new AsistenciaRepository(ctx);

            var result = await repo.YaIngresoHoyAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task YaIngresoHoyAsync_NoEntryToday_ReturnsFalse()
        {
            using var ctx = CreateDbContext();
            var a = new Asistencia(1, 1);
            typeof(Asistencia).GetProperty("FechaHoraAcceso")!.SetValue(a, DateTime.Today.AddDays(-1));
            ctx.Asistencias.Add(a);
            await ctx.SaveChangesAsync();
            var repo = new AsistenciaRepository(ctx);

            var result = await repo.YaIngresoHoyAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task YaIngresoHoyAsync_DifferentMember_ReturnsFalse()
        {
            using var ctx = CreateDbContext();
            var a = new Asistencia(2, 1);
            typeof(Asistencia).GetProperty("FechaHoraAcceso")!.SetValue(a, DateTime.Today.AddHours(10));
            ctx.Asistencias.Add(a);
            await ctx.SaveChangesAsync();
            var repo = new AsistenciaRepository(ctx);

            var result = await repo.YaIngresoHoyAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task ObtenerIngresosSemanaAsync_CountsCorrectly()
        {
            using var ctx = CreateDbContext();
            var a1 = new Asistencia(1, 1);
            typeof(Asistencia).GetProperty("FechaHoraAcceso")!.SetValue(a1, DateTime.Today);
            var a2 = new Asistencia(1, 1);
            typeof(Asistencia).GetProperty("FechaHoraAcceso")!.SetValue(a2, DateTime.Today.AddDays(-1));
            var a3 = new Asistencia(1, 1);
            typeof(Asistencia).GetProperty("FechaHoraAcceso")!.SetValue(a3, DateTime.Today.AddDays(-7));
            ctx.Asistencias.AddRange(a1, a2, a3);
            await ctx.SaveChangesAsync();
            var repo = new AsistenciaRepository(ctx);

            var result = await repo.ObtenerIngresosSemanaAsync(1);

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task ObtenerTodosAsync_ReturnsAll()
        {
            using var ctx = CreateDbContext();
            ctx.Asistencias.AddRange(
                new Asistencia(1, 1),
                new Asistencia(2, 1));
            await ctx.SaveChangesAsync();
            var repo = new AsistenciaRepository(ctx);

            var result = await repo.ObtenerTodosAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ObtenerTodosPorGimnasio_FiltersByGymViaMiembros()
        {
            using var ctx = CreateDbContext();
            var m1 = new Miembro("A", "a@t.com", "1", new DateOnly(2000, 1, 1), 1);
            var m2 = new Miembro("B", "b@t.com", "2", new DateOnly(2000, 1, 1), 2);
            ctx.Miembros.AddRange(m1, m2);
            await ctx.SaveChangesAsync();
            ctx.Asistencias.AddRange(
                new Asistencia(m1.Id, 1),
                new Asistencia(m2.Id, 1));
            await ctx.SaveChangesAsync();
            var repo = new AsistenciaRepository(ctx);

            var result = await repo.ObtenerTodosPorGimnasio(1);

            Assert.Single(result);
        }

        [Fact]
        public async Task ObtenerPorMiembroEnRango_RespectsGymAndDateRange()
        {
            using var ctx = CreateDbContext();
            var m = new Miembro("Test", "t@t.com", "3", new DateOnly(2000, 1, 1), 1);
            ctx.Miembros.Add(m);
            await ctx.SaveChangesAsync();

            var a1 = new Asistencia(m.Id, 1);
            typeof(Asistencia).GetProperty("FechaHoraAcceso")!.SetValue(a1, new DateTime(2026, 6, 1, 10, 0, 0));
            var a2 = new Asistencia(m.Id, 1);
            typeof(Asistencia).GetProperty("FechaHoraAcceso")!.SetValue(a2, new DateTime(2026, 6, 15, 10, 0, 0));
            ctx.Asistencias.AddRange(a1, a2);
            await ctx.SaveChangesAsync();
            var repo = new AsistenciaRepository(ctx);

            var result = await repo.ObtenerPorMiembroEnRango(
                m.Id, new DateTime(2026, 6, 1), new DateTime(2026, 6, 10), 1);

            Assert.Single(result);
        }
    }
}
