using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ControlFit.Application.DTO;
using ControlFit.Domain.Entidad;
using Microsoft.EntityFrameworkCore;

namespace ControlFit.Tests.Integration
{
    [Collection("IntegrationTests")]
    public class AsistenciaIntegrationTests : IntegrationTestBase
    {
        public AsistenciaIntegrationTests(ControlFitApiFactory factory) : base(factory) { }

        private async Task<(int memberId, int assignmentId)> CreateTestMemberWithAssignmentAsync(int maxIngresosSemana = 5)
        {
            SetAuthToken(GymAdminToken);

            var memberDto = new MiembroDTO
            {
                Nombre = $"Asistencia Member {Guid.NewGuid():N}",
                Correo = $"asistencia.{Guid.NewGuid():N}@test.com",
                Telefono = "111222333",
                FechaNacimiento = new DateOnly(1990, 1, 1),
                GimnasioId = GimnasioId
            };
            var memberResponse = await Client.PostAsJsonAsync("api/miembros/Registro", memberDto);
            var memberContent = await memberResponse.Content.ReadAsStringAsync();
            var memberDoc = JsonDocument.Parse(memberContent);
            var memberId = memberDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var membershipDto = new CrearMembresiaDTO
            {
                Nombre = $"Asistencia Membership {Guid.NewGuid():N}",
                Duración = 30,
                Precio = 49.99,
                MaximoIngresosPorDia = 10,
                MaximoIngresosPorSemana = maxIngresosSemana,
                MaximoIngresosTotales = 100,
                GimnasioId = GimnasioId
            };
            var membershipResponse = await Client.PostAsJsonAsync("api/membresia", membershipDto);
            var membershipContent = await membershipResponse.Content.ReadAsStringAsync();
            var membershipDoc = JsonDocument.Parse(membershipContent);
            var membershipId = membershipDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            ClearAuthToken();

            var assignDto = new AsignacionMembresiaCrearDTO { MiembroId = memberId, MembresiaId = membershipId };
            var assignResponse = await Client.PostAsJsonAsync("api/asignacion", assignDto);
            var assignContent = await assignResponse.Content.ReadAsStringAsync();
            var assignDoc = JsonDocument.Parse(assignContent);
            var assignmentId = assignDoc.RootElement.GetProperty("id").GetInt32();

            return (memberId, assignmentId);
        }

        [Fact]
        public async Task RegistrarIngreso_WithoutAuth_ReturnsUnauthorized()
        {
            var dto = new RegistroIngresoDTO { MiembroId = 1 };
            var response = await Client.PostAsJsonAsync("api/Asistencia/registrar", dto);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task RegistrarIngreso_InvalidMemberId_ReturnsBadRequest()
        {
            SetAuthToken(GymAdminToken);
            var dto = new RegistroIngresoDTO { MiembroId = 0 };
            var response = await Client.PostAsJsonAsync("api/Asistencia/registrar", dto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task RegistrarIngreso_SuccessFlow_ReturnsOk_AndStoresInDb()
        {
            var (memberId, _) = await CreateTestMemberWithAssignmentAsync();
            SetAuthToken(GymAdminToken);
            var dto = new RegistroIngresoDTO { MiembroId = memberId };

            var response = await Client.PostAsJsonAsync("api/Asistencia/registrar", dto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(body);
            var msg = doc.RootElement.GetProperty("message").GetString();
            Assert.Contains("Ingreso registrado exitosamente", msg, StringComparison.OrdinalIgnoreCase);

            using var db = CreateDbContext();
            var saved = await db.Asistencias
                .FirstOrDefaultAsync(a => a.MiembroId == memberId);
            Assert.NotNull(saved);
            Assert.Equal(memberId, saved.MiembroId);
        }

        [Fact]
        public async Task RegistrarIngreso_DuplicateEntry_ReturnsBadRequest_WithCorrectMessage()
        {
            var (memberId, _) = await CreateTestMemberWithAssignmentAsync();
            SetAuthToken(GymAdminToken);
            var dto = new RegistroIngresoDTO { MiembroId = memberId };

            var response1 = await Client.PostAsJsonAsync("api/Asistencia/registrar", dto);
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);

            var response2 = await Client.PostAsJsonAsync("api/Asistencia/registrar", dto);
            Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);

            var error = await GetResponseError(response2);
            Assert.Contains("ya ingresó hoy", error, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task RegistrarIngreso_ExpiredMembership_ReturnsBadRequest()
        {
            var (memberId, _) = await CreateTestMemberWithAssignmentAsync();

            using var db = CreateDbContext();
            db.Database.ExecuteSqlRaw(
                "UPDATE Asignacion SET FechaFin = DATEADD(day, -1, GETDATE()) WHERE MiembroId = {0}",
                memberId);

            SetAuthToken(GymAdminToken);
            var dto = new RegistroIngresoDTO { MiembroId = memberId };
            var response = await Client.PostAsJsonAsync("api/Asistencia/registrar", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var error = await GetResponseError(response);
            Assert.NotNull(error);
            Assert.Contains("vencida", error, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task RegistrarIngreso_WeeklyLimitExceeded_ReturnsBadRequest()
        {
            var (memberId, _) = await CreateTestMemberWithAssignmentAsync(maxIngresosSemana: 0);
            SetAuthToken(GymAdminToken);
            var dto = new RegistroIngresoDTO { MiembroId = memberId };

            var response = await Client.PostAsJsonAsync("api/Asistencia/registrar", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var error = await GetResponseError(response);
            Assert.Contains("Límite semanal alcanzado", error, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task RegistrarIngreso_MemberFromOtherGym_ReturnsBadRequest()
        {
            SetAuthToken(SuperAdminToken);
            var otherGymDto = new GimnasioCrearDTO($"Other Gym Asistencia {Guid.NewGuid():N}", "Other Address");
            var otherGymResponse = await Client.PostAsJsonAsync("api/Gym/Crear", otherGymDto);
            var otherGymContent = await otherGymResponse.Content.ReadAsStringAsync();
            var otherGymDoc = JsonDocument.Parse(otherGymContent);
            var otherGymId = otherGymDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var otherAdminEmail = $"other.asistencia.{Guid.NewGuid():N}@test.com";
            await Client.PostAsJsonAsync("api/auth/registro", new RegistroAdministradorDTO
            {
                nombreCompleto = "Other Gym Asistencia Admin",
                correo = otherAdminEmail,
                contrasena = "Test123456",
                GimnasioId = otherGymId
            });

            var loginResponse = await Client.PostAsJsonAsync("api/auth/login",
                new LoginAdministradorDTO { correo = otherAdminEmail, contrasena = "Test123456" });
            var loginContent = await loginResponse.Content.ReadAsStringAsync();
            var loginDoc = JsonDocument.Parse(loginContent);
            var otherAdminToken = loginDoc.RootElement.GetProperty("token").GetString();

            SetAuthToken(otherAdminToken!);
            var memberDto = new MiembroDTO
            {
                Nombre = "Other Gym Member",
                Correo = $"other.gym.asistencia.{Guid.NewGuid():N}@test.com",
                Telefono = "999888777",
                FechaNacimiento = new DateOnly(1992, 6, 6),
                GimnasioId = otherGymId
            };
            var memberResponse = await Client.PostAsJsonAsync("api/miembros/Registro", memberDto);
            var memberContent = await memberResponse.Content.ReadAsStringAsync();
            var memberDoc = JsonDocument.Parse(memberContent);
            var otherMemberId = memberDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var membershipDto = new CrearMembresiaDTO
            {
                Nombre = $"Other Gym Membership {Guid.NewGuid():N}",
                Duración = 30,
                Precio = 49.99,
                MaximoIngresosPorDia = 10,
                MaximoIngresosPorSemana = 5,
                MaximoIngresosTotales = 100,
                GimnasioId = otherGymId
            };
            var membershipResponse = await Client.PostAsJsonAsync("api/membresia", membershipDto);
            var membershipContent = await membershipResponse.Content.ReadAsStringAsync();
            var membershipDoc = JsonDocument.Parse(membershipContent);
            var otherMembershipId = membershipDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            ClearAuthToken();
            var assignDto = new AsignacionMembresiaCrearDTO { MiembroId = otherMemberId, MembresiaId = otherMembershipId };
            await Client.PostAsJsonAsync("api/asignacion", assignDto);

            SetAuthToken(GymAdminToken);
            var dto = new RegistroIngresoDTO { MiembroId = otherMemberId };
            var response = await Client.PostAsJsonAsync("api/Asistencia/registrar", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var error = await GetResponseError(response);
            Assert.NotNull(error);
        }

        [Fact]
        public async Task ObtenerAsistencias_AsGymAdmin_ReturnsOk()
        {
            SetAuthToken(GymAdminToken);
            var response = await Client.GetAsync("api/Asistencia");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task FiltrarAsistencias_ValidMember_ReturnsOk()
        {
            var (memberId, _) = await CreateTestMemberWithAssignmentAsync();
            SetAuthToken(GymAdminToken);

            await Client.PostAsJsonAsync("api/Asistencia/registrar", new RegistroIngresoDTO { MiembroId = memberId });

            var response = await Client.GetAsync($"api/Asistencia/filtrar?miembroId={memberId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
