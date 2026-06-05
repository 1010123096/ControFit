using ControlFit.Api.Controllers;
using ControlFit.Application.CasosUso.CRUDGimnasio;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControlFit.Tests.Api
{
    public class GimnasioControllerTests
    {
        private readonly GimnasioController _controller;
        private readonly Mock<IGimnasioRepository> _repoMock;
        private readonly Mock<IUserContextService> _userContextMock;

        public GimnasioControllerTests()
        {
            _repoMock = new Mock<IGimnasioRepository>();
            _userContextMock = new Mock<IUserContextService>();
            var service = new GimnasioService(_repoMock.Object, _userContextMock.Object);
            _controller = new GimnasioController(service);
        }

        [Fact]
        public async Task Crear_ValidDto_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _repoMock.Setup(x => x.ObtenerPorNombreAsync("Test")).ReturnsAsync((bool?)false);
            _repoMock.Setup(x => x.CrearAsync(It.IsAny<Gimnasio>())).ReturnsAsync(new Gimnasio("Test", "Addr", true));

            var dto = new GimnasioCrearDTO("Test", "Addr") { Estado = true };
            var result = await _controller.Crear(dto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Crear_ServiceThrows_ReturnsBadRequest()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);

            var dto = new GimnasioCrearDTO("", "") { Estado = true };
            var result = await _controller.Crear(dto) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerTodos_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _repoMock.Setup(x => x.ListarTodos()).ReturnsAsync(new List<Gimnasio?> { new Gimnasio("G1", "A", true) });

            var result = await _controller.ObtenerTodos() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerPorId_ValidId_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Gimnasio("Test", "Addr", true));

            var result = await _controller.ObtenerPorId(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Actualizar_Valid_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            var gym = new Gimnasio("Old", "OldAddr", true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(gym);
            _repoMock.Setup(x => x.ObtenerPorNombreAsync("New", 1)).ReturnsAsync((bool?)false);
            _repoMock.Setup(x => x.ActualizarAsync(It.IsAny<Gimnasio>())).ReturnsAsync(true);

            var dto = new GimnasioActualizarDTO { Id = 1, Nombre = "New", Direccion = "NewAddr", Estado = true };
            var result = await _controller.Actualizar(dto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Eliminar_ValidId_ReturnsOk()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Gimnasio("Test", "Addr", true));

            var result = await _controller.Eliminar(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
