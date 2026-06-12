using System.Net;
using System.Net.Http.Json;
using ControlFit.Application.DTO;

namespace ControlFit.Tests.Integration
{
    [Collection("IntegrationTests")]
    public class AuthIntegrationTests : IntegrationTestBase
    {
        public AuthIntegrationTests(ControlFitApiFactory factory) : base(factory) { }

        [Fact]
        public async Task Registrar_WithoutToken_ReturnsUnauthorized()
        {
            var dto = new RegistroAdministradorDTO
            {
                nombreCompleto = "New Super Admin",
                correo = $"new.super.{Guid.NewGuid():N}@test.com",
                contrasena = "Password123",
                GimnasioId = null
            };

            var response = await PostRegistroAsync(dto);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Registrar_ValidSuperAdmin_ReturnsOk()
        {
            var dto = new RegistroAdministradorDTO
            {
                nombreCompleto = "New Super Admin",
                correo = $"new.super.{Guid.NewGuid():N}@test.com",
                contrasena = "Password123",
                GimnasioId = null
            };

            var response = await PostRegistroAsync(dto, SuperAdminToken);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var message = await GetResponseMessage(response);
            Assert.NotNull(message);
        }

        [Fact]
        public async Task Registrar_ValidGymAdmin_ReturnsOk()
        {
            var dto = new RegistroAdministradorDTO
            {
                nombreCompleto = "New Gym Admin",
                correo = $"new.gymadmin.{Guid.NewGuid():N}@test.com",
                contrasena = "Password123",
                GimnasioId = GimnasioId
            };

            var response = await PostRegistroAsync(dto, SuperAdminToken);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Registrar_DuplicateEmail_ReturnsBadRequest()
        {
            var email = $"duplicate.{Guid.NewGuid():N}@test.com";
            var dto1 = new RegistroAdministradorDTO
            {
                nombreCompleto = "First Admin",
                correo = email,
                contrasena = "Password123",
                GimnasioId = null
            };
            await PostRegistroAsync(dto1, SuperAdminToken);

            var dto2 = new RegistroAdministradorDTO
            {
                nombreCompleto = "Second Admin",
                correo = email,
                contrasena = "Password123",
                GimnasioId = null
            };
            var response = await PostRegistroAsync(dto2, SuperAdminToken);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Registrar_EmptyNombre_ReturnsBadRequest()
        {
            var dto = new RegistroAdministradorDTO
            {
                nombreCompleto = "",
                correo = $"empty.nombre.{Guid.NewGuid():N}@test.com",
                contrasena = "Password123",
                GimnasioId = null
            };

            var response = await PostRegistroAsync(dto, SuperAdminToken);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Registrar_ShortPassword_ReturnsBadRequest()
        {
            var dto = new RegistroAdministradorDTO
            {
                nombreCompleto = "Short Password Admin",
                correo = $"short.pw.{Guid.NewGuid():N}@test.com",
                contrasena = "12345",
                GimnasioId = null
            };

            var response = await PostRegistroAsync(dto, SuperAdminToken);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_ValidSuperAdmin_ReturnsToken()
        {
            var dto = new LoginAdministradorDTO
            {
                correo = "super@test.com",
                contrasena = "Test123456"
            };

            var response = await Client.PostAsJsonAsync("api/auth/login", dto);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("token", content, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Login_ValidGymAdmin_ReturnsToken()
        {
            var dto = new LoginAdministradorDTO
            {
                correo = "gymadmin@test.com",
                contrasena = "Test123456"
            };

            var response = await Client.PostAsJsonAsync("api/auth/login", dto);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("token", content, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Login_InvalidPassword_ReturnsUnauthorized()
        {
            var dto = new LoginAdministradorDTO
            {
                correo = "super@test.com",
                contrasena = "WrongPassword"
            };

            var response = await Client.PostAsJsonAsync("api/auth/login", dto);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Login_UnknownUser_ReturnsUnauthorized()
        {
            var dto = new LoginAdministradorDTO
            {
                correo = "unknown@test.com",
                contrasena = "Password123"
            };

            var response = await Client.PostAsJsonAsync("api/auth/login", dto);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
