using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ControlFit.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Integration
{
    [Collection("IntegrationTests")]
    public class AsignacionIntegrationTests : IntegrationTestBase
    {
        public AsignacionIntegrationTests(ControlFitApiFactory factory) : base(factory) { }

        private async Task<(int memberId, int membershipId)> CreateTestData()
        {
            SetAuthToken(GymAdminToken);

            var memberDto = new MiembroDTO
            {
                Nombre = $"Asignacion Member {Guid.NewGuid():N}",
                Correo = $"asignacion.{Guid.NewGuid():N}@test.com",
                Telefono = "123456789",
                FechaNacimiento = new DateOnly(1990, 1, 1),
                GimnasioId = GimnasioId
            };
            var memberResponse = await Client.PostAsJsonAsync("api/miembros/Registro", memberDto);
            var memberContent = await memberResponse.Content.ReadAsStringAsync();
            var memberDoc = JsonDocument.Parse(memberContent);
            var memberId = memberDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var membershipDto = new CrearMembresiaDTO
            {
                Nombre = $"Asignacion Membership {Guid.NewGuid():N}",
                Duración = 30,
                Precio = 49.99,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 50,
                GimnasioId = GimnasioId
            };
            var membershipResponse = await Client.PostAsJsonAsync("api/membresia", membershipDto);
            var membershipContent = await membershipResponse.Content.ReadAsStringAsync();
            var membershipDoc = JsonDocument.Parse(membershipContent);
            var membershipId = membershipDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            ClearAuthToken();

            return (memberId, membershipId);
        }

        [Fact]
        public async Task Crear_ValidAssignment_ReturnsOk_AndStoredInDb()
        {
            var (memberId, membershipId) = await CreateTestData();

            var dto = new AsignacionMembresiaCrearDTO
            {
                MiembroId = memberId,
                MembresiaId = membershipId
            };

            var response = await Client.PostAsJsonAsync("api/asignacion", dto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(body);
            var id = doc.RootElement.GetProperty("id").GetInt32();
            Assert.True(id > 0);
            Assert.Equal(memberId, doc.RootElement.GetProperty("miembroId").GetInt32());
            Assert.Equal(membershipId, doc.RootElement.GetProperty("membresiaId").GetInt32());
            Assert.True(doc.RootElement.GetProperty("estado").GetBoolean());

            using var db = CreateDbContext();
            var saved = await db.AsignacionesMembresia.FindAsync(id);
            Assert.NotNull(saved);
            Assert.Equal(memberId, saved.MiembroId);
            Assert.Equal(membershipId, saved.MembresiaId);
            Assert.True(saved.Estado);
        }

        [Fact]
        public async Task Crear_DuplicateActiveAssignment_ReturnsError()
        {
            var (memberId, membershipId) = await CreateTestData();

            var dto = new AsignacionMembresiaCrearDTO
            {
                MiembroId = memberId,
                MembresiaId = membershipId
            };

            await Client.PostAsJsonAsync("api/asignacion", dto);
            var response = await Client.PostAsJsonAsync("api/asignacion", dto);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Crear_NonExistentMember_ReturnsError()
        {
            var (_, membershipId) = await CreateTestData();

            var dto = new AsignacionMembresiaCrearDTO
            {
                MiembroId = 9999,
                MembresiaId = membershipId
            };

            var response = await Client.PostAsJsonAsync("api/asignacion", dto);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Crear_NonExistentMembership_ReturnsError()
        {
            var (memberId, _) = await CreateTestData();

            var dto = new AsignacionMembresiaCrearDTO
            {
                MiembroId = memberId,
                MembresiaId = 9999
            };

            var response = await Client.PostAsJsonAsync("api/asignacion", dto);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerTodos_ReturnsAssignments()
        {
            var response = await Client.GetAsync("api/asignacion");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerPorId_ValidId_ReturnsAssignment_WithCorrectJson()
        {
            var (memberId, membershipId) = await CreateTestData();
            var createDto = new AsignacionMembresiaCrearDTO { MiembroId = memberId, MembresiaId = membershipId };
            var createResponse = await Client.PostAsJsonAsync("api/asignacion", createDto);
            var content = await createResponse.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(content);
            var id = doc.RootElement.GetProperty("id").GetInt32();

            var response = await Client.GetAsync($"api/asignacion/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var doc2 = JsonDocument.Parse(body);
            Assert.Equal(id, doc2.RootElement.GetProperty("id").GetInt32());
            Assert.Equal(memberId, doc2.RootElement.GetProperty("miembroId").GetInt32());
            Assert.Equal(membershipId, doc2.RootElement.GetProperty("membresiaId").GetInt32());
        }

        [Fact]
        public async Task ObtenerPorId_InvalidId_ReturnsError()
        {
            var response = await Client.GetAsync("api/asignacion/9999");

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Eliminar_ValidId_RemovesFromDb()
        {
            var (memberId, membershipId) = await CreateTestData();
            var createDto = new AsignacionMembresiaCrearDTO { MiembroId = memberId, MembresiaId = membershipId };
            var createResponse = await Client.PostAsJsonAsync("api/asignacion", createDto);
            var content = await createResponse.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(content);
            var id = doc.RootElement.GetProperty("id").GetInt32();

            var response = await Client.DeleteAsync($"api/asignacion/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var db = CreateDbContext();
            var saved = await db.AsignacionesMembresia.FindAsync(id);
            Assert.Null(saved);
        }
    }
}
