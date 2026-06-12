using System.Net;
using System.Net.Http.Json;
using ControlFit.Application.DTO;

namespace ControlFit.Tests.Integration
{
    [Collection("IntegrationTests")]
    public class FunctionalTests : IntegrationTestBase
    {
        public FunctionalTests(ControlFitApiFactory factory) : base(factory) { }

        [Fact]
        public async Task CompleteFlow_CreateGym_RegisterAdmin_CreateMember_AssignMembership_CheckIn()
        {
            // 1. Create a new gym (as SuperAdmin)
            SetAuthToken(SuperAdminToken);
            var gymDto = new GimnasioCrearDTO($"Functional Flow Gym {Guid.NewGuid():N}", "123 Main St");
            var gymResponse = await Client.PostAsJsonAsync("api/gimnasios/Registro", gymDto);
            Assert.Equal(HttpStatusCode.OK, gymResponse.StatusCode);
            var gymContent = await gymResponse.Content.ReadAsStringAsync();
            var gymDoc = System.Text.Json.JsonDocument.Parse(gymContent);
            var newGymId = gymDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            // 2. Register a gym admin for the new gym
            var adminEmail = $"functional.admin.{Guid.NewGuid():N}@test.com";
            var adminDto = new RegistroAdministradorDTO
            {
                nombreCompleto = "Functional Flow Admin",
                correo = adminEmail,
                contrasena = "Password123",
                GimnasioId = newGymId
            };
            var adminResponse = await PostRegistroAsync(adminDto, SuperAdminToken);
            Assert.Equal(HttpStatusCode.OK, adminResponse.StatusCode);

            // 3. Login as the new gym admin
            var loginDto = new LoginAdministradorDTO
            {
                correo = adminEmail,
                contrasena = "Password123"
            };
            var loginResponse = await Client.PostAsJsonAsync("api/auth/login", loginDto);
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            var loginContent = await loginResponse.Content.ReadAsStringAsync();
            var loginDoc = System.Text.Json.JsonDocument.Parse(loginContent);
            var token = loginDoc.RootElement.GetProperty("token").GetString();

            // 4. Create a membership as the gym admin
            SetAuthToken(token!);
            var membershipDto = new CrearMembresiaDTO
            {
                Nombre = $"Functional Membership {Guid.NewGuid():N}",
                Duración = 30,
                Precio = 99.99,
                MaximoIngresosPorDia = 2,
                MaximoIngresosPorSemana = 10,
                MaximoIngresosTotales = 100,
                GimnasioId = newGymId
            };
            var membershipResponse = await Client.PostAsJsonAsync("api/membresia", membershipDto);
            Assert.Equal(HttpStatusCode.OK, membershipResponse.StatusCode);
            var membershipContent = await membershipResponse.Content.ReadAsStringAsync();
            var membershipDoc = System.Text.Json.JsonDocument.Parse(membershipContent);
            var membershipId = membershipDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            // 5. Create a member
            var memberDto = new MiembroDTO
            {
                Nombre = "Functional Flow Member",
                Correo = $"functional.member.{Guid.NewGuid():N}@test.com",
                Telefono = "5551234567",
                FechaNacimiento = new DateOnly(1990, 1, 15),
                GimnasioId = newGymId
            };
            var memberResponse = await Client.PostAsJsonAsync("api/miembros/Registro", memberDto);
            Assert.Equal(HttpStatusCode.OK, memberResponse.StatusCode);
            var memberContent = await memberResponse.Content.ReadAsStringAsync();
            var memberDoc = System.Text.Json.JsonDocument.Parse(memberContent);
            var memberId = memberDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            // 6. Assign membership
            var assignDto = new AsignacionMembresiaCrearDTO
            {
                MiembroId = memberId,
                MembresiaId = membershipId
            };
            var assignResponse = await Client.PostAsJsonAsync("api/asignacion", assignDto);
            Assert.Equal(HttpStatusCode.OK, assignResponse.StatusCode);

            // 7. Register check-in as gym admin
            SetAuthToken(token!);
            var checkinDto = new RegistroIngresoDTO { MiembroId = memberId };
            var checkinResponse = await Client.PostAsJsonAsync("api/Asistencia/registrar", checkinDto);
            Assert.Equal(HttpStatusCode.OK, checkinResponse.StatusCode);

            // 8. Validate duplicate check-in is rejected
            var duplicateCheckinResponse = await Client.PostAsJsonAsync("api/Asistencia/registrar", checkinDto);
            Assert.Equal(HttpStatusCode.BadRequest, duplicateCheckinResponse.StatusCode);

            // 9. Query member history
            var historyResponse = await Client.GetAsync($"api/Historial/miembro/{memberId}");
            Assert.Equal(HttpStatusCode.OK, historyResponse.StatusCode);

            // 10. Query attendance history
            var attendanceHistoryResponse = await Client.GetAsync($"api/Historial/asistencias?miembroId={memberId}");
            Assert.Equal(HttpStatusCode.OK, attendanceHistoryResponse.StatusCode);
        }

        [Fact]
        public async Task BusinessRule_SuperAdmin_CannotCreateMembers()
        {
            SetAuthToken(SuperAdminToken);

            var memberDto = new MiembroDTO
            {
                Nombre = "Super Admin Member",
                Correo = $"super.member.{Guid.NewGuid():N}@test.com",
                Telefono = "5550001111",
                FechaNacimiento = new DateOnly(1985, 5, 5),
                GimnasioId = GimnasioId
            };
            var response = await Client.PostAsJsonAsync("api/miembros/Registro", memberDto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var error = await GetResponseError(response);
            Assert.Contains("Super Admin no puede crear miembros", error, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task BusinessRule_SuperAdmin_CannotCreateMemberships()
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
            var error = await GetResponseError(response);
            Assert.Contains("Super Admin no puede crear membresías", error, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task BusinessRule_GymAdmin_CannotCreateGyms()
        {
            SetAuthToken(GymAdminToken);

            var dto = new GimnasioCrearDTO($"Gym Admin Gym {Guid.NewGuid():N}", "Address");

            var response = await Client.PostAsJsonAsync("api/gimnasios/Registro", dto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task BusinessRule_MemberCannotCheckInWithoutActiveMembership()
        {
            SetAuthToken(GymAdminToken);

            var memberDto = new MiembroDTO
            {
                Nombre = "No Membership Member",
                Correo = $"no.membership.{Guid.NewGuid():N}@test.com",
                Telefono = "5556667777",
                FechaNacimiento = new DateOnly(1995, 3, 3),
                GimnasioId = GimnasioId
            };
            var memberResponse = await Client.PostAsJsonAsync("api/miembros/Registro", memberDto);
            var memberContent = await memberResponse.Content.ReadAsStringAsync();
            var memberDoc = System.Text.Json.JsonDocument.Parse(memberContent);
            var memberId = memberDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            var checkinDto = new RegistroIngresoDTO { MiembroId = memberId };
            var response = await Client.PostAsJsonAsync("api/Asistencia/registrar", checkinDto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task BusinessRule_GymAdmin_OnlySeesOwnGymMembers()
        {
            // 1. GymAdmin creates a member in their own gym
            SetAuthToken(GymAdminToken);
            var seedMemberDto = new MiembroDTO
            {
                Nombre = "Seed Gym Member",
                Correo = $"seed.gym.{Guid.NewGuid():N}@test.com",
                Telefono = "5551112222",
                FechaNacimiento = new DateOnly(1990, 1, 1),
                GimnasioId = GimnasioId
            };
            var seedMemberResponse = await Client.PostAsJsonAsync("api/miembros/Registro", seedMemberDto);
            var seedMemberContent = await seedMemberResponse.Content.ReadAsStringAsync();
            var seedMemberDoc = System.Text.Json.JsonDocument.Parse(seedMemberContent);
            var seedMemberId = seedMemberDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            // 2. SuperAdmin creates a new gym
            SetAuthToken(SuperAdminToken);
            var gymDto = new GimnasioCrearDTO($"Other Gym {Guid.NewGuid():N}", "456 Other St");
            var gymResponse = await Client.PostAsJsonAsync("api/gimnasios/Registro", gymDto);
            var gymContent = await gymResponse.Content.ReadAsStringAsync();
            var gymDoc = System.Text.Json.JsonDocument.Parse(gymContent);
            var otherGymId = gymDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            // 3. Register a new admin for the other gym
            var otherAdminEmail = $"other.admin.{Guid.NewGuid():N}@test.com";
            var adminRegDto = new RegistroAdministradorDTO
            {
                nombreCompleto = "Other Gym Admin",
                correo = otherAdminEmail,
                contrasena = "Test123456",
                GimnasioId = otherGymId
            };
            await PostRegistroAsync(adminRegDto, SuperAdminToken);

            // 4. Login as the other gym admin and create a member there
            var loginDto = new LoginAdministradorDTO { correo = otherAdminEmail, contrasena = "Test123456" };
            var loginResponse = await Client.PostAsJsonAsync("api/auth/login", loginDto);
            var loginContent = await loginResponse.Content.ReadAsStringAsync();
            var loginDoc = System.Text.Json.JsonDocument.Parse(loginContent);
            var otherAdminToken = loginDoc.RootElement.GetProperty("token").GetString();

            SetAuthToken(otherAdminToken!);
            var otherMemberDto = new MiembroDTO
            {
                Nombre = "Other Gym Member",
                Correo = $"other.gym.{Guid.NewGuid():N}@test.com",
                Telefono = "5553334444",
                FechaNacimiento = new DateOnly(1992, 3, 3),
                GimnasioId = otherGymId
            };
            var otherMemberResponse = await Client.PostAsJsonAsync("api/miembros/Registro", otherMemberDto);
            var otherMemberContent = await otherMemberResponse.Content.ReadAsStringAsync();
            var otherMemberDoc = System.Text.Json.JsonDocument.Parse(otherMemberContent);
            var otherMemberId = otherMemberDoc.RootElement.GetProperty("data").GetProperty("id").GetInt32();

            // 5. Re-auth as seed GymAdmin and verify isolation
            SetAuthToken(GymAdminToken);
            var response = await Client.GetAsync("api/miembros/obtenerTodos");
            var content = await response.Content.ReadAsStringAsync();
            var doc = System.Text.Json.JsonDocument.Parse(content);
            var miembros = doc.RootElement.GetProperty("data").EnumerateArray().ToList();
            var memberIds = miembros.Select(m => m.GetProperty("id").GetInt32()).ToList();

            Assert.Contains(seedMemberId, memberIds);
            Assert.DoesNotContain(otherMemberId, memberIds);
        }

        [Fact]
        public async Task BusinessRule_SuperAdmin_SeesAllMembers()
        {
            SetAuthToken(GymAdminToken);

            var memberDto = new MiembroDTO
            {
                Nombre = "Visible Member",
                Correo = $"visible.{Guid.NewGuid():N}@test.com",
                Telefono = "1112223333",
                FechaNacimiento = new DateOnly(1992, 7, 7),
                GimnasioId = GimnasioId
            };
            await Client.PostAsJsonAsync("api/miembros/Registro", memberDto);

            SetAuthToken(SuperAdminToken);
            var response = await Client.GetAsync("api/miembros/obtenerTodos");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var doc = System.Text.Json.JsonDocument.Parse(content);
            var total = doc.RootElement.GetProperty("total").GetInt32();
            Assert.True(total >= 1);
        }
    }
}
