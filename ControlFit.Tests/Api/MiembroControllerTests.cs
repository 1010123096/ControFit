using ControlFit.Api.Controllers;
using ControlFit.Application.CasosUso.CRUDMiembro;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Application.Repository;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControlFit.Tests.Api
{
    public class MiembroControllerTests
    {
        private readonly MiembroController _controller;
        private readonly Mock<IMiembroRepository> _repoMock;
        private readonly Mock<IUserContextService> _userContextMock;

        public MiembroControllerTests()
        {
            _repoMock = new Mock<IMiembroRepository>();
            _userContextMock = new Mock<IUserContextService>();
            var tokenServiceMock = new Mock<ITokenService>();
            var service = new MiembroService(_repoMock.Object, _userContextMock.Object);
            _controller = new MiembroController(service);
        }

        [Fact]
        public async Task Crear_ValidDto_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _repoMock.Setup(x => x.CrearAsync(It.IsAny<Miembro>())).ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var dto = new MiembroDTO { Nombre = "Test", Correo = "t@t.com", Telefono = "999", FechaNacimiento = new DateOnly(1990, 1, 1), GimnasioId = 1 };
            var result = await _controller.Crear(dto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Crear_ServiceThrows_ReturnsBadRequest()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);

            var dto = new MiembroDTO { Nombre = "Test", Correo = "t@t.com", Telefono = "999", FechaNacimiento = new DateOnly(1990, 1, 1), GimnasioId = 0 };
            var result = await _controller.Crear(dto) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerTodos_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ListarTodos()).ReturnsAsync(new List<Miembro>());

            var result = await _controller.ObtenerTodos() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerPorId_ValidId_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var result = await _controller.ObtenerPorId(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Actualizar_Valid_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Miembro("Old", "old@t.com", "111", new DateOnly(1990, 1, 1), 1));
            _repoMock.Setup(x => x.ActualizarAsync(It.IsAny<Miembro>())).ReturnsAsync(true);

            var dto = new MiembroDTO { Id = 1, Nombre = "New", Correo = "new@t.com", Telefono = "222", FechaNacimiento = new DateOnly(1990, 1, 1), GimnasioId = 1 };
            var result = await _controller.Actualizar(dto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Eliminar_ValidId_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var result = await _controller.Eliminar(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
