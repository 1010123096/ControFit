using ControlFit.Api.Controllers;
using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.DTO;
using ControlFit.Application.Repository;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControlFit.Tests.Api
{
    public class AutenticacionControllerTests
    {
        [Fact]
        public async Task Login_ValidCredentials_ReturnsOk()
        {
            var repoMock = new Mock<IAdministradorRepository>();
            var tokenServiceMock = new Mock<ITokenService>();
            var loginService = new LoginAdministrador(repoMock.Object, tokenServiceMock.Object);

            var admin = new Administrador("Admin", "admin@test.com", BCrypt.Net.BCrypt.HashPassword("pass"), null);
            repoMock.Setup(x => x.ObtenerPorCorreoAsync("admin@test.com")).ReturnsAsync(admin);
            tokenServiceMock.Setup(x => x.GenerarToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string?>())).Returns("jwt-token");

            var registrarMock = new Mock<RegistrarAdministrador>(Mock.Of<IAdministradorRepository>());
            var controller = new AutenticacionController(registrarMock.Object, loginService);

            var dto = new LoginAdministradorDTO { correo = "admin@test.com", contrasena = "pass" };
            var result = await controller.Login(dto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            var repoMock = new Mock<IAdministradorRepository>();
            var tokenServiceMock = new Mock<ITokenService>();
            var loginService = new LoginAdministrador(repoMock.Object, tokenServiceMock.Object);

            var admin = new Administrador("Admin", "admin@test.com", BCrypt.Net.BCrypt.HashPassword("correctpass"), null);
            repoMock.Setup(x => x.ObtenerPorCorreoAsync("admin@test.com")).ReturnsAsync(admin);

            var registrarMock = new Mock<RegistrarAdministrador>(Mock.Of<IAdministradorRepository>());
            var controller = new AutenticacionController(registrarMock.Object, loginService);

            var dto = new LoginAdministradorDTO { correo = "admin@test.com", contrasena = "wrongpass" };
            var result = await controller.Login(dto) as UnauthorizedObjectResult;

            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public async Task Login_UserNotFound_ReturnsUnauthorized()
        {
            var repoMock = new Mock<IAdministradorRepository>();
            var tokenServiceMock = new Mock<ITokenService>();
            var loginService = new LoginAdministrador(repoMock.Object, tokenServiceMock.Object);

            repoMock.Setup(x => x.ObtenerPorCorreoAsync("unknown@test.com")).ReturnsAsync((Administrador?)null);

            var registrarMock = new Mock<RegistrarAdministrador>(Mock.Of<IAdministradorRepository>());
            var controller = new AutenticacionController(registrarMock.Object, loginService);

            var dto = new LoginAdministradorDTO { correo = "unknown@test.com", contrasena = "pass" };
            var result = await controller.Login(dto) as UnauthorizedObjectResult;

            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public async Task Registrar_ValidDto_ReturnsOk()
        {
            var repoMock = new Mock<IAdministradorRepository>();
            repoMock.Setup(x => x.ObtenerPorCorreoAsync("new@test.com")).ReturnsAsync((Administrador?)null);
            var registrarService = new RegistrarAdministrador(repoMock.Object);

            var loginMock = new Mock<LoginAdministrador>(Mock.Of<IAdministradorRepository>(), Mock.Of<ITokenService>());
            var controller = new AutenticacionController(registrarService, loginMock.Object);

            var dto = new RegistroAdministradorDTO { nombreCompleto = "New Admin", correo = "new@test.com", contrasena = "123456", GimnasioId = 1 };
            var result = await controller.Registrar(dto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
