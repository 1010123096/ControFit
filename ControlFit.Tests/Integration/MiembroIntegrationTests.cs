using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ControlFit.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Integration
{
    [Collection("IntegrationTests")]
    public class MiembroIntegrationTests : IntegrationTestBase
    {
        public MiembroIntegrationTests(ControlFitApiFactory factory) : base(factory) { }

        [Fact]
        public async Task Crear_WithoutAuth_ReturnsUnauthorized()
        {
            var dto = new MiembroDTO
            {
                Nombre = "John Doe",
                Correo = $"john.{Guid.NewGuid():N}@test.com",
                Telefono = "123456789",
                FechaNacimiento = new DateOnly(1990, 1, 1),
                GimnasioId = GimnasioId
            };

            var response = await Client.PostAsJsonAsync("api/miembros/Registro", dto);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Crear_AsGymAdmin_ValidMember_ReturnsOk_WithCorrectJson_AndStoredInDb()
        {
            SetAuthToken(GymAdminToken);

            var miembroNombre = "Jane Doe";
            var miembroCorreo = $"jane.{Guid.NewGuid():N}@test.com";
            var dto = new MiembroDTO
            {
                Nombre = miembroNombre,
                Correo = miembroCorreo,
                Telefono = "987654321",
                FechaNacimiento = new DateOnly(1995, 5, 15),
                GimnasioId = GimnasioId
            };

            var response = await Client.PostAsJsonAsync("api/miembros/Registro", dto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(body);
            var msg = doc.RootElement.GetProperty("message").GetString();
            Assert.Contains("creado exitosamente", msg, StringComparison.OrdinalIgnoreCase);

            var data = doc.RootElement.GetProperty("data");
            var id = data.GetProperty("id").GetInt32();
            Assert.True(id > 0);
            Assert.Equal(miembroNombre, data.GetProperty("nombre").GetString());
            Assert.Equal(miembroCorreo, data.GetProperty("correo").GetString());
            Assert.True(data.GetProperty("estado").GetBoolean());

            using var db = CreateDbContext();
            var saved = await db.Miembros.FindAsync(id);
            Assert.NotNull(saved);
            Assert.Equal(miembroNombre, saved.Nombre);
            Assert.Equal(miembroCorreo, saved.Correo);
        }

        [Fact]
        public async Task Crear_AsSuperAdmin_ReturnsBadRequest()
        {
            SetAuthToken(SuperAdminToken);

            var dto = new MiembroDTO
            {
                Nombre = "Super Member",
                Correo = $"super.member.{Guid.NewGuid():N}@test.com",
                Telefono = "555555555",
                FechaNacimiento = new DateOnly(1985, 3, 20),
                GimnasioId = GimnasioId
            };

            var response = await Client.PostAsJsonAsync("api/miembros/Registro", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Crear_WithWrongGymId_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var dto = new MiembroDTO
            {
                Nombre = "Wrong Gym Member",
                Correo = $"wrong.gym.{Guid.NewGuid():N}@test.com",
                Telefono = "111111111",
                FechaNacimiento = new DateOnly(2000, 7, 7),
                GimnasioId = 9999
            };

            var response = await Client.PostAsJsonAsync("api/miembros/Registro", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerTodos_AsGymAdmin_ReturnsOk()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/miembros/obtenerTodos");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerTodos_AsSuperAdmin_ReturnsOk()
        {
            SetAuthToken(SuperAdminToken);

            var response = await Client.GetAsync("api/miembros/obtenerTodos");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerPorId_InvalidId_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/miembros/obtenerporID?id=9999");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateAndGet_ReturnsCreatedMember_WithCorrectData()
        {
            SetAuthToken(GymAdminToken);

            var createDto = new MiembroDTO
            {
                Nombre = "CreateGet Member",
                Correo = $"createget.{Guid.NewGuid():N}@test.com",
                Telefono = "222222222",
                FechaNacimiento = new DateOnly(1992, 8, 8),
                GimnasioId = GimnasioId
            };
            var createResponse = await Client.PostAsJsonAsync("api/miembros/Registro", createDto);
            var createContent = await createResponse.Content.ReadAsStringAsync();
            var createDoc = JsonDocument.Parse(createContent);
            var memberId = createDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var response = await Client.GetAsync($"api/miembros/obtenerporID?id={memberId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(body);
            var data = doc.RootElement.GetProperty("data");
            Assert.Equal(memberId, data.GetProperty("id").GetInt32());
            Assert.Equal("CreateGet Member", data.GetProperty("nombre").GetString());
            Assert.Equal(createDto.Correo, data.GetProperty("correo").GetString());
        }

        [Fact]
        public async Task Actualizar_AsGymAdmin_UpdatesInDb()
        {
            SetAuthToken(GymAdminToken);

            var createDto = new MiembroDTO
            {
                Nombre = "Updatable Member",
                Correo = $"updatable.{Guid.NewGuid():N}@test.com",
                Telefono = "333333333",
                FechaNacimiento = new DateOnly(1993, 9, 9),
                GimnasioId = GimnasioId
            };
            var createResponse = await Client.PostAsJsonAsync("api/miembros/Registro", createDto);
            var createContent = await createResponse.Content.ReadAsStringAsync();
            var createDoc = JsonDocument.Parse(createContent);
            var memberId = createDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var updatedCorreo = $"updated.{Guid.NewGuid():N}@test.com";
            var updateDto = new MiembroDTO
            {
                Id = memberId,
                Nombre = "Updated Member",
                Correo = updatedCorreo,
                Telefono = "444444444",
                FechaNacimiento = new DateOnly(1993, 9, 9),
                GimnasioId = GimnasioId
            };

            var response = await Client.PutAsJsonAsync("api/miembros/actualizar", updateDto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var db = CreateDbContext();
            var saved = await db.Miembros.FindAsync(memberId);
            Assert.NotNull(saved);
            Assert.Equal("Updated Member", saved.Nombre);
            Assert.Equal(updatedCorreo, saved.Correo);
            Assert.Equal("444444444", saved.Telefono);
        }

        [Fact]
        public async Task Eliminar_AsGymAdmin_RemovesFromDb()
        {
            SetAuthToken(GymAdminToken);

            var createDto = new MiembroDTO
            {
                Nombre = "Deletable Member",
                Correo = $"deletable.{Guid.NewGuid():N}@test.com",
                Telefono = "555555555",
                FechaNacimiento = new DateOnly(1994, 10, 10),
                GimnasioId = GimnasioId
            };
            var createResponse = await Client.PostAsJsonAsync("api/miembros/Registro", createDto);
            var createContent = await createResponse.Content.ReadAsStringAsync();
            var createDoc = JsonDocument.Parse(createContent);
            var memberId = createDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var response = await Client.DeleteAsync($"api/miembros/eliminar?id={memberId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var db = CreateDbContext();
            var saved = await db.Miembros.FindAsync(memberId);
            Assert.Null(saved);
        }

        [Fact]
        public async Task Eliminar_InvalidId_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.DeleteAsync("api/miembros/eliminar?id=9999");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
