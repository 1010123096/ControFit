using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ControlFit.Application.DTO;
using ControlFit.Domain.Entidad;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Integration
{
    [Collection("IntegrationTests")]
    public class MembresiaIntegrationTests : IntegrationTestBase
    {
        public MembresiaIntegrationTests(ControlFitApiFactory factory) : base(factory) { }

        [Fact]
        public async Task Crear_WithoutAuth_ReturnsUnauthorized()
        {
            var dto = new CrearMembresiaDTO
            {
                Nombre = "Unauthorized Membership",
                Duración = 30,
                Precio = 99.99,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 100,
                GimnasioId = GimnasioId
            };

            var response = await Client.PostAsJsonAsync("api/membresia", dto);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Crear_AsGymAdmin_ReturnsOk_CreatesInDb_WithCorrectData()
        {
            SetAuthToken(GymAdminToken);

            var nombre = $"Monthly Membership {Guid.NewGuid():N}";
            var dto = new CrearMembresiaDTO
            {
                Nombre = nombre,
                Duración = 30,
                Precio = 99.99,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 100,
                GimnasioId = GimnasioId
            };

            var response = await Client.PostAsJsonAsync("api/membresia", dto);
            var body = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var doc = JsonDocument.Parse(body);
            var msg = doc.RootElement.GetProperty("message").GetString();
            Assert.Contains("Creada exitosamente", msg, StringComparison.OrdinalIgnoreCase);

            var data = doc.RootElement.GetProperty("data");
            var id = data.GetProperty("id").GetInt32();
            Assert.True(id > 0);
            Assert.Equal(nombre, data.GetProperty("nombre").GetString());
            Assert.Equal(30, data.GetProperty("duracionDias").GetInt32());
            Assert.Equal(99.99, data.GetProperty("precio").GetDouble(), 1);
            Assert.Equal(GimnasioId, data.GetProperty("gimnasioId").GetInt32());

            using var db = CreateDbContext();
            var saved = await db.Membresias.FindAsync(id);
            Assert.NotNull(saved);
            Assert.Equal(nombre, saved.Nombre);
            Assert.Equal(30, saved.Duración);
            Assert.Equal(99.99, saved.Precio, 1);
            Assert.Equal(GimnasioId, saved.GimnasioId);
            Assert.True(saved.Estado);
        }

        [Fact]
        public async Task Crear_AsSuperAdmin_ReturnsBadRequest()
        {
            SetAuthToken(SuperAdminToken);

            var dto = new CrearMembresiaDTO
            {
                Nombre = "Super Admin Membership",
                Duración = 30,
                Precio = 49.99,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 50,
                GimnasioId = GimnasioId
            };

            var response = await Client.PostAsJsonAsync("api/membresia", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Crear_WithInvalidGymId_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var dto = new CrearMembresiaDTO
            {
                Nombre = "Wrong Gym Membership",
                Duración = 30,
                Precio = 49.99,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 50,
                GimnasioId = 9999
            };

            var response = await Client.PostAsJsonAsync("api/membresia", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerTodas_AsGymAdmin_ReturnsMemberships()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/membresia");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerTodas_AsSuperAdmin_ReturnsAllMemberships()
        {
            SetAuthToken(SuperAdminToken);

            var response = await Client.GetAsync("api/membresia");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerPorId_AsGymAdmin_ValidId_ReturnsMembership_WithCorrectJson()
        {
            SetAuthToken(GymAdminToken);

            var createDto = new CrearMembresiaDTO
            {
                Nombre = $"Membership To Get {Guid.NewGuid():N}",
                Duración = 30,
                Precio = 79.99,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 50,
                GimnasioId = GimnasioId
            };
            var createResponse = await Client.PostAsJsonAsync("api/membresia", createDto);
            var newId = await ExtractIdFromResponse(createResponse, "data.id");

            var response = await Client.GetAsync($"api/membresia/{newId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(body);
            var data = doc.RootElement.GetProperty("data");
            Assert.Equal(createDto.Nombre, data.GetProperty("nombre").GetString());
            Assert.Equal(30, data.GetProperty("duracionDias").GetInt32());
            Assert.Equal(79.99, data.GetProperty("precio").GetDouble(), 1);
            Assert.True(data.GetProperty("estado").GetBoolean());
        }

        [Fact]
        public async Task ObtenerPorId_InvalidId_ReturnsNotFound()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/membresia/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Actualizar_AsGymAdmin_ValidData_UpdatesInDb_WithCorrectData()
        {
            SetAuthToken(GymAdminToken);

            var createDto = new CrearMembresiaDTO
            {
                Nombre = $"Membership To Update {Guid.NewGuid():N}",
                Duración = 30,
                Precio = 59.99,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 50,
                GimnasioId = GimnasioId
            };
            var createResponse = await Client.PostAsJsonAsync("api/membresia", createDto);
            var newId = await ExtractIdFromResponse(createResponse, "data.id");

            var nombresActualizado = $"Updated Membership {Guid.NewGuid():N}";
            var updateDto = new ActualizarMembresiaDTO
            {
                Nombre = nombresActualizado,
                Duración = 60,
                Precio = 129.99,
                Estado = true,
                MaximoIngresosPorDia = 2,
                MaximoIngresosPorSemana = 10,
                MaximoIngresosTotales = 200,
                GimnasioId = GimnasioId
            };

            var response = await Client.PutAsJsonAsync($"api/membresia/{newId}", updateDto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(body);
            var msg = doc.RootElement.GetProperty("message").GetString();
            Assert.Contains("actualizada exitosamente", msg, StringComparison.OrdinalIgnoreCase);

            using var db = CreateDbContext();
            var saved = await db.Membresias.FindAsync(newId);
            Assert.NotNull(saved);
            Assert.Equal(nombresActualizado, saved.Nombre);
            Assert.Equal(60, saved.Duración);
            Assert.Equal(129.99, saved.Precio, 1);
            Assert.Equal(2, saved.MaximoIngresosPorDia);
            Assert.Equal(10, saved.MaximoIngresosPorSemana);
        }

        [Fact]
        public async Task Eliminar_AsGymAdmin_ValidId_RemovesFromDb()
        {
            SetAuthToken(GymAdminToken);

            var createDto = new CrearMembresiaDTO
            {
                Nombre = $"Membership To Delete {Guid.NewGuid():N}",
                Duración = 30,
                Precio = 39.99,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 50,
                GimnasioId = GimnasioId
            };
            var createResponse = await Client.PostAsJsonAsync("api/membresia", createDto);
            var newId = await ExtractIdFromResponse(createResponse, "data.id");

            var response = await Client.DeleteAsync($"api/membresia/{newId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var db = CreateDbContext();
            var saved = await db.Membresias.FindAsync(newId);
            Assert.Null(saved);
        }

        private async Task<int> ExtractIdFromResponse(HttpResponseMessage response, string path)
        {
            var content = await response.Content.ReadAsStringAsync();
            var doc = System.Text.Json.JsonDocument.Parse(content);
            var parts = path.Split('.');
            var element = doc.RootElement;
            foreach (var part in parts)
            {
                element = element.GetProperty(part);
            }
            return element.GetInt32();
        }
    }
}
