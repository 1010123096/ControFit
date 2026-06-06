using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.DTO;
using ControlFit.Application.Repository;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Moq;

namespace ControlFit.Tests.Application
{
    public class LoginAdministradorTests
    {
        private readonly Mock<IAdministradorRepository> _repoMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly LoginAdministrador _service;

        public LoginAdministradorTests()
        {
            _repoMock = new Mock<IAdministradorRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _service = new LoginAdministrador(_repoMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task EjecutarAsyncLogin_ValidCredentials_ReturnsToken()
        {
            var admin = new Administrador("Admin", "admin@test.com", BCrypt.Net.BCrypt.HashPassword("correctpass"), 1);
            _repoMock.Setup(x => x.ObtenerPorCorreoAsync("admin@test.com")).ReturnsAsync(admin);
            _tokenServiceMock.Setup(x => x.GenerarToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string?>())).Returns("jwt-token");

            var dto = new LoginAdministradorDTO { correo = "admin@test.com", contrasena = "correctpass" };
            var result = await _service.EjecutarAsyncLogin(dto);

            Assert.Equal("jwt-token", result);
        }

        [Fact]
        public async Task EjecutarAsyncLogin_InvalidPassword_ReturnsNull()
        {
            var admin = new Administrador("Admin", "admin@test.com", BCrypt.Net.BCrypt.HashPassword("correctpass"), 1);
            _repoMock.Setup(x => x.ObtenerPorCorreoAsync("admin@test.com")).ReturnsAsync(admin);

            var dto = new LoginAdministradorDTO { correo = "admin@test.com", contrasena = "wrongpass" };
            var result = await _service.EjecutarAsyncLogin(dto);

            Assert.Null(result);
        }

        [Fact]
        public async Task EjecutarAsyncLogin_UserNotFound_ReturnsNull()
        {
            _repoMock.Setup(x => x.ObtenerPorCorreoAsync("unknown@test.com")).ReturnsAsync((Administrador?)null);

            var dto = new LoginAdministradorDTO { correo = "unknown@test.com", contrasena = "pass" };
            var result = await _service.EjecutarAsyncLogin(dto);

            Assert.Null(result);
        }
    }
}
