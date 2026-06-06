using ControlFit.Domain.Entidad;
using ControlFit.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ControlFit.Tests.Infrastructure
{
    public class AppDbContextTests
    {
        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public void OnModelCreating_Administrador_HasCorrectTableName()
        {
            using var ctx = CreateDbContext();
            var entityType = ctx.Model.FindEntityType(typeof(Administrador));
            Assert.NotNull(entityType);
            Assert.Equal("Administrador", entityType.GetTableName());
        }

        [Fact]
        public void OnModelCreating_Administrador_HasKeyOnId()
        {
            using var ctx = CreateDbContext();
            var entityType = ctx.Model.FindEntityType(typeof(Administrador));
            var key = entityType.FindPrimaryKey();
            Assert.NotNull(key);
            Assert.Contains(key.Properties, p => p.Name == "Id");
        }

        [Fact]
        public void OnModelCreating_Administrador_NombreCompleto_HasMaxLength100()
        {
            using var ctx = CreateDbContext();
            var entityType = ctx.Model.FindEntityType(typeof(Administrador));
            var prop = entityType.FindProperty("NombreCompleto");
            Assert.NotNull(prop);
            Assert.Equal(100, prop.GetMaxLength());
            Assert.False(prop.IsNullable);
        }

        [Fact]
        public void OnModelCreating_Administrador_Correo_HasMaxLength100()
        {
            using var ctx = CreateDbContext();
            var entityType = ctx.Model.FindEntityType(typeof(Administrador));
            var prop = entityType.FindProperty("Correo");
            Assert.NotNull(prop);
            Assert.Equal(100, prop.GetMaxLength());
        }

        [Fact]
        public void OnModelCreating_Administrador_Contrasena_HasMaxLength255()
        {
            using var ctx = CreateDbContext();
            var entityType = ctx.Model.FindEntityType(typeof(Administrador));
            var prop = entityType.FindProperty("Contrasena");
            Assert.NotNull(prop);
            Assert.Equal(255, prop.GetMaxLength());
            Assert.False(prop.IsNullable);
        }

        [Fact]
        public void OnModelCreating_Miembro_HasCorrectTableName()
        {
            using var ctx = CreateDbContext();
            var entityType = ctx.Model.FindEntityType(typeof(Miembro));
            Assert.NotNull(entityType);
            Assert.Equal("Miembro", entityType.GetTableName());
        }

        [Fact]
        public void OnModelCreating_Gimnasio_HasCorrectTableName()
        {
            using var ctx = CreateDbContext();
            var entityType = ctx.Model.FindEntityType(typeof(Gimnasio));
            Assert.NotNull(entityType);
            Assert.Equal("Gimnasio", entityType.GetTableName());
        }

        [Fact]
        public void OnModelCreating_Membresia_HasCorrectTableName()
        {
            using var ctx = CreateDbContext();
            var entityType = ctx.Model.FindEntityType(typeof(Membresia));
            Assert.NotNull(entityType);
            Assert.Equal("Membresia", entityType.GetTableName());
        }

        [Fact]
        public void OnModelCreating_AsignacionMembresia_HasCorrectTableName()
        {
            using var ctx = CreateDbContext();
            var entityType = ctx.Model.FindEntityType(typeof(AsignacionMembresia));
            Assert.NotNull(entityType);
            Assert.Equal("Asignacion", entityType.GetTableName());
        }

        [Fact]
        public void OnModelCreating_Asistencia_HasCorrectTableName()
        {
            using var ctx = CreateDbContext();
            var entityType = ctx.Model.FindEntityType(typeof(Asistencia));
            Assert.NotNull(entityType);
            Assert.Equal("Asistencia", entityType.GetTableName());
        }

        [Fact]
        public void OnModelCreating_AllEntities_HavePrimaryKey()
        {
            using var ctx = CreateDbContext();
            var entityTypes = new[] {
                typeof(Administrador),
                typeof(Miembro),
                typeof(Gimnasio),
                typeof(Membresia),
                typeof(AsignacionMembresia),
                typeof(Asistencia)
            };
            foreach (var type in entityTypes)
            {
                var entityType = ctx.Model.FindEntityType(type);
                Assert.NotNull(entityType);
                Assert.NotNull(entityType.FindPrimaryKey());
            }
        }

        [Fact]
        public async Task DbSets_CanAddAndRetrieve_Administrador()
        {
            using var ctx = CreateDbContext();
            ctx.Administradores.Add(new Administrador("Test", "t@t.com", "hash"));
            await ctx.SaveChangesAsync();
            Assert.Equal(1, await ctx.Administradores.CountAsync());
        }

        [Fact]
        public async Task DbSets_CanAddAndRetrieve_Miembro()
        {
            using var ctx = CreateDbContext();
            ctx.Miembros.Add(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));
            await ctx.SaveChangesAsync();
            Assert.Equal(1, await ctx.Miembros.CountAsync());
        }

        [Fact]
        public async Task DbSets_CanAddAndRetrieve_Gimnasio()
        {
            using var ctx = CreateDbContext();
            ctx.gimnasios.Add(new Gimnasio("Gym", "Addr", true));
            await ctx.SaveChangesAsync();
            Assert.Equal(1, await ctx.gimnasios.CountAsync());
        }

        [Fact]
        public async Task DbSets_CanAddAndRetrieve_Membresia()
        {
            using var ctx = CreateDbContext();
            ctx.Membresias.Add(new Membresia("Premium", 30, 99.99, 0, 0, 0, true, 1));
            await ctx.SaveChangesAsync();
            Assert.Equal(1, await ctx.Membresias.CountAsync());
        }

        [Fact]
        public async Task DbSets_CanAddAndRetrieve_AsignacionMembresia()
        {
            using var ctx = CreateDbContext();
            ctx.Membresias.Add(new Membresia("Premium", 30, 99.99, 0, 0, 0, true, 1));
            await ctx.SaveChangesAsync();
            var membresia = await ctx.Membresias.FirstAsync();
            ctx.AsignacionesMembresia.Add(new AsignacionMembresia(1, membresia));
            await ctx.SaveChangesAsync();
            Assert.Equal(1, await ctx.AsignacionesMembresia.CountAsync());
        }

        [Fact]
        public async Task DbSets_CanAddAndRetrieve_Asistencia()
        {
            using var ctx = CreateDbContext();
            ctx.Asistencias.Add(new Asistencia(1, 1));
            await ctx.SaveChangesAsync();
            Assert.Equal(1, await ctx.Asistencias.CountAsync());
        }
    }
}
