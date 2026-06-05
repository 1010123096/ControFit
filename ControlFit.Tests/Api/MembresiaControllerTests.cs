using ControlFit.Api.Controllers;
using ControlFit.Application.CasosUso.CRUDMembresia;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControlFit.Tests.Api
{
    public class MembresiaControllerTests
    {
        private readonly MembresiaController _controller;
        private readonly Mock<IMembresiaRepository> _repoMock;
        private readonly Mock<IUserContextService> _userContextMock;

        public MembresiaControllerTests()
        {
            _repoMock = new Mock<IMembresiaRepository>();
            _userContextMock = new Mock<IUserContextService>();
            var service = new MembresiaService(_repoMock.Object, _userContextMock.Object);
            _controller = new MembresiaController(service);
        }

        [Fact]
        public async Task Crear_ValidDto_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _repoMock.Setup(x => x.CrearAsync(It.IsAny<Membresia>())).ReturnsAsync(
                new Membresia("Test", 30, 100, 2, 10, 50, true, 1));

            var dto = new CrearMembresiaDTO { Nombre = "Test", Duración = 30, Precio = 100, MaximoIngresosPorDia = 2, MaximoIngresosPorSemana = 10, MaximoIngresosTotales = 50, GimnasioId = 1 };
            var actionResult = await _controller.Crear(dto);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Crear_ServiceThrows_ReturnsBadRequest()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);

            var dto = new CrearMembresiaDTO { Nombre = "Test", Duración = 30, Precio = 100, GimnasioId = 1 };
            var actionResult = await _controller.Crear(dto);
            var result = actionResult.Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerTodas_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ListarTodos()).ReturnsAsync(new List<Membresia>());

            var result = await _controller.ObtenerTodas() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerPorId_ValidId_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Membresia("Test", 30, 100));

            var actionResult = await _controller.ObtenerPorId(1);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Actualizar_Valid_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Membresia("Old", 30, 100, 2, 10, 50, true, 1));
            _repoMock.Setup(x => x.ActualizarAsync(It.IsAny<Membresia>())).ReturnsAsync(true);

            var dto = new ActualizarMembresiaDTO { Id = 1, Nombre = "New", Duración = 60, Precio = 200, MaximoIngresosPorDia = 3, MaximoIngresosPorSemana = 15, MaximoIngresosTotales = 100, Estado = true, GimnasioId = 1 };
            var actionResult = await _controller.Actualizar(1, dto);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Eliminar_ValidId_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Membresia("Test", 30, 100));

            var result = await _controller.Eliminar(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
