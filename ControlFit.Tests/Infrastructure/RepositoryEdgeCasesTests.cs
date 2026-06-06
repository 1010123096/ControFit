using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Infrastructure.Persistencia;
using ControlFit.Infrastructure.Repository;
using ControlFit.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Infrastructure
{
    public class RepositoryEdgeCasesTests
    {
        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task AdministradorRepository_ObtenerTodosAsync_ReturnsAll()
        {
            using var ctx = CreateDbContext();
            ctx.Administradores.AddRange(
                new Administrador("Admin1", "a1@t.com", "hash1"),
                new Administrador("Admin2", "a2@t.com", "hash2"));
            await ctx.SaveChangesAsync();
            var repo = new AdministradorRepository(ctx);

            var result = await repo.ObtenerTodosAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AdministradorRepository_ObtenerTodosAsync_Empty_ReturnsEmpty()
        {
            using var ctx = CreateDbContext();
            var repo = new AdministradorRepository(ctx);

            var result = await repo.ObtenerTodosAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task MembresiaRepository_EliminarAsync_Existing_Removes()
        {
            using var ctx = CreateDbContext();
            var m = new Membresia("Test", 30, 50.0, 0, 0, 0, true, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();
            var repo = new MembresiaRepository(ctx);

            await repo.EliminarAsync(m.Id);

            Assert.Equal(0, await ctx.Membresias.CountAsync());
        }

        [Fact]
        public async Task MembresiaRepository_EliminarAsync_NonExisting_DoesNotThrow()
        {
            using var ctx = CreateDbContext();
            var repo = new MembresiaRepository(ctx);

            await repo.EliminarAsync(999);

            Assert.Equal(0, await ctx.Membresias.CountAsync());
        }

        [Fact]
        public async Task MembresiaRepository_ObtenerPorIdValidandoGimnasio_SameGym_ReturnsMembresia()
        {
            using var ctx = CreateDbContext();
            var m = new Membresia("Test", 30, 50.0, 0, 0, 0, true, 3);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();
            var repo = new MembresiaRepository(ctx);

            var result = await repo.ObtenerPorIdValidandoGimnasio(m.Id, 3);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Nombre);
        }

        [Fact]
        public async Task GimnasioRepository_ObtenerPorNombreAsync_WithIdActual_AnotherGymHasName_ReturnsTrue()
        {
            using var ctx = CreateDbContext();
            ctx.gimnasios.AddRange(
                new Gimnasio("SameName", "Addr1", true),
                new Gimnasio("Other", "Addr2", true));
            await ctx.SaveChangesAsync();
            var gym2 = await ctx.gimnasios.FirstAsync(g => g.Nombre == "Other");
            var repo = new GimnasioRepository(ctx);

            var result = await repo.ObtenerPorNombreAsync("SameName", gym2.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task GimnasioRepository_ObtenerPorNombreAsync_WithNombre_IgnoresCase()
        {
            using var ctx = CreateDbContext();
            ctx.gimnasios.Add(new Gimnasio("MiGym", "Addr", true));
            await ctx.SaveChangesAsync();
            var repo = new GimnasioRepository(ctx);

            var result = await repo.ObtenerPorNombreAsync("migym");

            Assert.True(result);
        }

        [Fact]
        public async Task GimnasioRepository_ObtenerPorNombreAsync_WithNombre_TrimsSpaces()
        {
            using var ctx = CreateDbContext();
            ctx.gimnasios.Add(new Gimnasio("MiGym", "Addr", true));
            await ctx.SaveChangesAsync();
            var repo = new GimnasioRepository(ctx);

            var result = await repo.ObtenerPorNombreAsync("  MiGym  ");

            Assert.True(result);
        }

        [Fact]
        public async Task MiembroRepository_ListarTodosPorGimnasio_EmptyGym_ReturnsEmpty()
        {
            using var ctx = CreateDbContext();
            ctx.Miembros.Add(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));
            await ctx.SaveChangesAsync();
            var repo = new MiembroRepository(ctx);

            var result = await repo.ListarTodosPorGimnasio(999);

            Assert.Empty(result);
        }

        [Fact]
        public async Task AsistenciaRepository_YaIngresoHoyAsync_NeverIngressed_ReturnsFalse()
        {
            using var ctx = CreateDbContext();
            var repo = new AsistenciaRepository(ctx);

            var result = await repo.YaIngresoHoyAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task AsistenciaRepository_ObtenerIngresosSemanaAsync_NoEntries_ReturnsZero()
        {
            using var ctx = CreateDbContext();
            var repo = new AsistenciaRepository(ctx);

            var result = await repo.ObtenerIngresosSemanaAsync(1);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task AsistenciaRepository_ObtenerPorMiembroEnRango_GimnasioMismatch_ReturnsEmpty()
        {
            using var ctx = CreateDbContext();
            var m = new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1);
            ctx.Miembros.Add(m);
            await ctx.SaveChangesAsync();
            var a = new Asistencia(m.Id, 1);
            typeof(Asistencia).GetProperty("FechaHoraAcceso")!.SetValue(a, DateTime.Now);
            ctx.Asistencias.Add(a);
            await ctx.SaveChangesAsync();
            var repo = new AsistenciaRepository(ctx);

            var result = await repo.ObtenerPorMiembroEnRango(m.Id, DateTime.MinValue, DateTime.MaxValue, 999);

            Assert.Empty(result);
        }

        [Fact]
        public async Task AsignacionMembresiaRepository_ObtenerActivaAsync_EstadoInactivo_ReturnsNull()
        {
            using var ctx = CreateDbContext();
            var m = new Membresia("Test", 30, 50.0, 0, 0, 0, true, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();
            var a = new AsignacionMembresia(1, m);
            typeof(AsignacionMembresia).GetProperty("Estado")!.SetValue(a, false);
            ctx.AsignacionesMembresia.Add(a);
            await ctx.SaveChangesAsync();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.ObtenerActivaAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task AsignacionMembresiaRepository_ObtenerTodosAsync_Empty_ReturnsEmpty()
        {
            using var ctx = CreateDbContext();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.ObtenerTodosAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task AsignacionMembresiaRepository_ObtenerTodosPorGimnasio_NoGimnasio_ReturnsEmpty()
        {
            using var ctx = CreateDbContext();
            var m = new Membresia("Test", 30, 50.0, 0, 0, 0, true, 1);
            ctx.Membresias.Add(m);
            await ctx.SaveChangesAsync();
            ctx.AsignacionesMembresia.Add(new AsignacionMembresia(1, m));
            await ctx.SaveChangesAsync();
            var repo = new AsignacionMembresiaRepository(ctx);

            var result = await repo.ObtenerTodosPorGimnasio(999);

            Assert.Empty(result);
        }
    }
}
