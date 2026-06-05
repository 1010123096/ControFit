using ControlFit.Application.Repository;
using ControlFit.Domain.Entidad;
using ControlFit.Infrastructure.Persistencia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace ControlFit.Tests.Integration
{
    public class SeedData
    {
        public int GimnasioId { get; set; }
        public int SuperAdminId { get; set; }
        public int GymAdminId { get; set; }
        public string SuperAdminToken { get; set; } = string.Empty;
        public string GymAdminToken { get; set; } = string.Empty;
    }

    public class ControlFitApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        public string DatabaseName { get; }
        private readonly string _masterConnectionString;
        private readonly string _connectionString;
        private bool _initialized;
        private static readonly object _lock = new();

        public SeedData SeedData { get; private set; } = new();

        public ControlFitApiFactory()
        {
            DatabaseName = $"FitControl_Test_{Guid.NewGuid():N}";
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = @"KEMTOKU\SQLEXPRESS",
                IntegratedSecurity = true,
                TrustServerCertificate = true,
                MultipleActiveResultSets = true
            };
            _masterConnectionString = new SqlConnectionStringBuilder(builder.ConnectionString)
            {
                InitialCatalog = "master",
                ConnectTimeout = 15
            }.ConnectionString;
            _connectionString = new SqlConnectionStringBuilder(builder.ConnectionString)
            {
                InitialCatalog = DatabaseName,
                ConnectTimeout = 30
            }.ConnectionString;
        }

        public string GetConnectionString() => _connectionString;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSetting("ConnectionStrings:DefaultConnection", _connectionString);
            builder.UseSetting("Jwt:Key", "12345678901234567890123456789012");
            builder.UseSetting("Jwt:Issuer", "ControlFitIssuer");
            builder.UseSetting("Jwt:Audience", "ControlFitAudience");

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null) services.Remove(descriptor);
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(_connectionString));
            });
        }

        public async Task InitializeAsync()
        {
            if (_initialized) return;
            lock (_lock)
            {
                if (_initialized) return;
            }

            await CreateDatabaseAsync();
            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.EnsureCreatedAsync();
            await SeedInitialDataAsync(scope.ServiceProvider);

            _initialized = true;
        }

        private async Task CreateDatabaseAsync()
        {
            using var connection = new SqlConnection(_masterConnectionString);
            await connection.OpenAsync();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = @DbName) CREATE DATABASE [{DatabaseName}]";
            cmd.Parameters.AddWithValue("@DbName", DatabaseName);
            cmd.CommandTimeout = 60;
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task SeedInitialDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var gym = new Gimnasio("Test Gym", "Test Address", true);
            context.gimnasios.Add(gym);
            await context.SaveChangesAsync();
            SeedData.GimnasioId = gym.Id;

            var superAdmin = new Administrador("Super Admin Test", "super@test.com", "", null);
            superAdmin.AsignarContrasena("Test123456");
            context.Administradores.Add(superAdmin);
            await context.SaveChangesAsync();
            SeedData.SuperAdminId = superAdmin.Id;

            var gymAdmin = new Administrador("Gym Admin Test", "gymadmin@test.com", "", SeedData.GimnasioId);
            gymAdmin.AsignarContrasena("Test123456");
            context.Administradores.Add(gymAdmin);
            await context.SaveChangesAsync();
            SeedData.GymAdminId = gymAdmin.Id;

            var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
            SeedData.SuperAdminToken = tokenService.GenerarToken(superAdmin.Id, superAdmin.Correo, null);
            SeedData.GymAdminToken = tokenService.GenerarToken(gymAdmin.Id, gymAdmin.Correo, SeedData.GimnasioId);
        }

        public new async Task DisposeAsync()
        {
            try
            {
                SqlConnection.ClearAllPools();
                await DropDatabaseAsync();
            }
            catch { }
        }

        private async Task DropDatabaseAsync()
        {
            using var connection = new SqlConnection(_masterConnectionString);
            await connection.OpenAsync();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = $@"
                IF EXISTS (SELECT * FROM sys.databases WHERE name = @DbName)
                BEGIN
                    ALTER DATABASE [{DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE [{DatabaseName}]
                END";
            cmd.Parameters.AddWithValue("@DbName", DatabaseName);
            cmd.CommandTimeout = 60;
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
