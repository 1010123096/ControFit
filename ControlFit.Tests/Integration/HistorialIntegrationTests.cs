using System.Net;
using System.Net.Http.Json;
using ControlFit.Application.DTO;

namespace ControlFit.Tests.Integration
{
    [Collection("IntegrationTests")]
    public class HistorialIntegrationTests : IntegrationTestBase
    {
        public HistorialIntegrationTests(ControlFitApiFactory factory) : base(factory) { }

        private async Task<int> CreateMemberWithAssignment()
        {
            SetAuthToken(GymAdminToken);

            var memberDto = new MiembroDTO
            {
                Nombre = $"Historial Member {Guid.NewGuid():N}",
                Correo = $"historial.{Guid.NewGuid():N}@test.com",
                Telefono = "111222333",
                FechaNacimiento = new DateOnly(1990, 1, 1),
                GimnasioId = GimnasioId
            };
            var memberResponse = await Client.PostAsJsonAsync("api/miembros/Registro", memberDto);
            var memberContent = await memberResponse.Content.ReadAsStringAsync();
            var memberDoc = System.Text.Json.JsonDocument.Parse(memberContent);
            var memberId = memberDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var membershipDto = new CrearMembresiaDTO
            {
                Nombre = $"Historial Membership {Guid.NewGuid():N}",
                Duración = 30,
                Precio = 49.99,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 50,
                GimnasioId = GimnasioId
            };
            var membershipResponse = await Client.PostAsJsonAsync("api/membresia", membershipDto);
            var membershipContent = await membershipResponse.Content.ReadAsStringAsync();
            var membershipDoc = System.Text.Json.JsonDocument.Parse(membershipContent);
            var membershipId = membershipDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            ClearAuthToken();

            var assignDto = new AsignacionMembresiaCrearDTO
            {
                MiembroId = memberId,
                MembresiaId = membershipId
            };
            await Client.PostAsJsonAsync("api/asignacion", assignDto);

            return memberId;
        }

        [Fact]
        public async Task ObtenerHistorialMembresias_WithoutAuth_ReturnsUnauthorized()
        {
            var response = await Client.GetAsync("api/Historial/membresias?miembroId=1");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialMembresias_AsGymAdmin_ValidMember_ReturnsOk()
        {
            var memberId = await CreateMemberWithAssignment();
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync($"api/Historial/membresias?miembroId={memberId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialMembresias_InvalidMemberId_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/Historial/membresias?miembroId=0");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialAsignaciones_AsGymAdmin_ValidMember_ReturnsOk()
        {
            var memberId = await CreateMemberWithAssignment();
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync($"api/Historial/asignaciones?miembroId={memberId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialAsistencias_AsGymAdmin_ValidMember_ReturnsOk()
        {
            var memberId = await CreateMemberWithAssignment();
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync($"api/Historial/asistencias?miembroId={memberId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialAsistencias_WithDateRange_ReturnsOk()
        {
            var memberId = await CreateMemberWithAssignment();
            SetAuthToken(GymAdminToken);
            var fromDate = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            var toDate = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");

            var response = await Client.GetAsync(
                $"api/Historial/asistencias?miembroId={memberId}&fechaInicio={fromDate}&fechaFin={toDate}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerResumenMiembro_AsGymAdmin_ValidMember_ReturnsOk()
        {
            var memberId = await CreateMemberWithAssignment();
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync($"api/Historial/miembro/{memberId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerResumenMiembro_InvalidMemberId_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/Historial/miembro/0");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ObtenerResumenMiembro_NonExistentMember_ReturnsNotFound()
        {
            SetAuthToken(SuperAdminToken);

            var response = await Client.GetAsync("api/Historial/miembro/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
