using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ControlFit.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Integration
{
    [Collection("IntegrationTests")]
    public class GimnasioIntegrationTests : IntegrationTestBase
    {
        public GimnasioIntegrationTests(ControlFitApiFactory factory) : base(factory) { }

        [Fact]
        public async Task Crear_ValidGym_ReturnsOk_WithCorrectJson_AndStoredInDb()
        {
            var nombre = $"New Gym {Guid.NewGuid():N}";
            var dto = new GimnasioCrearDTO(nombre, "Some Address");

            var response = await Client.PostAsJsonAsync("api/Gym/Crear", dto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(body);
            var msg = doc.RootElement.GetProperty("message").GetString();
            Assert.Contains("Gimnasio creado exitosamente", msg, StringComparison.OrdinalIgnoreCase);

            var data = doc.RootElement.GetProperty("data");
            var id = data.GetProperty("id").GetInt32();
            Assert.True(id > 0);
            Assert.Equal(nombre, data.GetProperty("nombre").GetString());
            Assert.Equal("Some Address", data.GetProperty("direccion").GetString());
            Assert.True(data.GetProperty("estado").GetBoolean());

            using var db = CreateDbContext();
            var saved = await db.gimnasios.FindAsync(id);
            Assert.NotNull(saved);
            Assert.Equal(nombre, saved.Nombre);
            Assert.Equal("Some Address", saved.Direccion);
            Assert.True(saved.Estado);
        }

        [Fact]
        public async Task Crear_DuplicateName_ReturnsBadRequest()
        {
            var name = $"Duplicate Gym {Guid.NewGuid():N}";
            var dto1 = new GimnasioCrearDTO(name, "Address 1");
            await Client.PostAsJsonAsync("api/Gym/Crear", dto1);

            var dto2 = new GimnasioCrearDTO(name, "Address 2");
            var response = await Client.PostAsJsonAsync("api/Gym/Crear", dto2);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var error = await GetResponseError(response);
            Assert.NotNull(error);
        }

        [Fact]
        public async Task Crear_EmptyName_ReturnsBadRequest()
        {
            var dto = new GimnasioCrearDTO("", "Some Address");

            var response = await Client.PostAsJsonAsync("api/Gym/Crear", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerTodos_ReturnsGyms()
        {
            var response = await Client.GetAsync("api/Gym/ObtenerTodos");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerPorId_ValidId_ReturnsOk()
        {
            var response = await Client.GetAsync($"api/Gym/ObtenerPorId?id={GimnasioId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerPorId_InvalidId_ReturnsBadRequest()
        {
            var response = await Client.GetAsync("api/Gym/ObtenerPorId?id=9999");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Actualizar_ValidData_UpdatesInDb()
        {
            var nuevoNombre = $"Updated Gym {Guid.NewGuid():N}";
            var dto = new GimnasioActualizarDTO
            {
                Id = GimnasioId,
                Nombre = nuevoNombre,
                Direccion = "Updated Address",
                Estado = true
            };

            var response = await Client.PutAsJsonAsync("api/Gym/Actualizar", dto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var db = CreateDbContext();
            var saved = await db.gimnasios.FindAsync(GimnasioId);
            Assert.NotNull(saved);
            Assert.Equal(nuevoNombre, saved.Nombre);
            Assert.Equal("Updated Address", saved.Direccion);
            Assert.True(saved.Estado);
        }

        [Fact]
        public async Task Actualizar_InvalidId_ReturnsBadRequest()
        {
            var dto = new GimnasioActualizarDTO
            {
                Id = 9999,
                Nombre = "Non-existent Gym",
                Direccion = "Address",
                Estado = true
            };

            var response = await Client.PutAsJsonAsync("api/Gym/Actualizar", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Eliminar_ValidId_RemovesFromDb()
        {
            var createResponse = await Client.PostAsJsonAsync(
                "api/Gym/Crear",
                new GimnasioCrearDTO($"Gym to Delete {Guid.NewGuid():N}", "Address"));
            var content = await createResponse.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(content);
            var newId = doc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var response = await Client.DeleteAsync($"api/Gym/Eliminar?id={newId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var db = CreateDbContext();
            var saved = await db.gimnasios.FindAsync(newId);
            Assert.Null(saved);
        }

        [Fact]
        public async Task Eliminar_InvalidId_ReturnsBadRequest()
        {
            var response = await Client.DeleteAsync("api/Gym/Eliminar?id=9999");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
