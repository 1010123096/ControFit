using System.Net;
using System.Net.Http.Json;
using ControlFit.Application.DTO;

namespace ControlFit.Tests.Integration
{
    [Collection("IntegrationTests")]
    public class IntegrationEdgeCasesTests : IntegrationTestBase
    {
        public IntegrationEdgeCasesTests(ControlFitApiFactory factory) : base(factory) { }

        [Fact]
        public async Task Gimnasio_GetNonExistent_ReturnsBadRequest()
        {
            SetAuthToken(SuperAdminToken);

            var response = await Client.GetAsync("api/gimnasios/obtenerPorId?id=99999");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Gimnasio_DeleteNonExistent_ReturnsBadRequest()
        {
            SetAuthToken(SuperAdminToken);

            var response = await Client.DeleteAsync("api/gimnasios/eliminar?id=99999");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Membresia_DuplicateName_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var name = $"Unique Membresia Dup {Guid.NewGuid():N}";
            var dto = new CrearMembresiaDTO
            {
                Nombre = name,
                Duración = 30,
                Precio = 50,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 100,
                GimnasioId = GimnasioId
            };

            await Client.PostAsJsonAsync("api/membresia", dto);
            var response = await Client.PostAsJsonAsync("api/membresia", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Membresia_GetNonExistent_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/membresia/99999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Membresia_DeleteNonExistent_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.DeleteAsync("api/membresia/99999");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Miembro_GetNonExistent_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/miembros/obtenerporID?id=99999");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Miembro_DeleteNonExistent_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.DeleteAsync("api/miembros/eliminar?id=99999");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Asignacion_Create_WithNonExistentMember_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var membresiaDto = new CrearMembresiaDTO
            {
                Nombre = $"Memb for Asignacion {Guid.NewGuid():N}",
                Duración = 30,
                Precio = 50,
                MaximoIngresosPorDia = 1,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 100,
                GimnasioId = GimnasioId
            };
            var membresiaResponse = await Client.PostAsJsonAsync("api/membresia", membresiaDto);
            var membresiaContent = await membresiaResponse.Content.ReadAsStringAsync();
            var membresiaDoc = System.Text.Json.JsonDocument.Parse(membresiaContent);
            var membresiaId = membresiaDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var dto = new Dictionary<string, object>
            {
                ["miembroId"] = 99999,
                ["membresiaId"] = membresiaId
            };
            var response = await Client.PostAsJsonAsync("api/asignacion", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Asignacion_GetNonExistent_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/asignacion/99999");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Asistencia_Register_WithoutAuth_ReturnsUnauthorized()
        {
            ClearAuthToken();

            var dto = new { miembroId = 1, asignacionMembresiaId = 1 };
            var response = await Client.PostAsJsonAsync("api/Asistencia/registrar", dto);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Asistencia_Register_NonExistentMember_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var dto = new { miembroId = 99999, asignacionMembresiaId = 1 };
            var response = await Client.PostAsJsonAsync("api/Asistencia/registrar", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Historial_Membresias_InvalidId_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/Historial/membresias?miembroId=0");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Historial_Asistencias_InvalidId_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/Historial/asistencias?miembroId=-1");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Historial_Resumen_InvalidId_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/Historial/miembro/0");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Historial_Todos_GymAdmin_ReturnsOk()
        {
            SetAuthToken(GymAdminToken);

            var response = await Client.GetAsync("api/Historial");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Historial_Todos_SuperAdmin_ReturnsOk()
        {
            SetAuthToken(SuperAdminToken);

            var response = await Client.GetAsync("api/Historial");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
