using ControlFit.Api.Controllers;
using ControlFit.Application.CasosUso.CRUDAsignacion;
using ControlFit.Application.DTO;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControlFit.Tests.Api
{
    public class AsignacionMembresiaControllerTests
    {
        private readonly AsignacionMembresiaController _controller;
        private readonly Mock<IAsignacionMembresiaRepository> _repoMock;
        private readonly Mock<IMembresiaRepository> _membresiaRepoMock;
        private readonly Mock<IMiembroRepository> _miembroRepoMock;

        public AsignacionMembresiaControllerTests()
        {
            _repoMock = new Mock<IAsignacionMembresiaRepository>();
            _membresiaRepoMock = new Mock<IMembresiaRepository>();
            _miembroRepoMock = new Mock<IMiembroRepository>();
            var service = new AsignacionMembresiaService(_repoMock.Object, _membresiaRepoMock.Object, _miembroRepoMock.Object);
            _controller = new AsignacionMembresiaController(service);
        }

        [Fact]
        public async Task Crear_ValidDto_ReturnsOk()
        {
            var miembro = new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1);
            var membresia = new Membresia("Premium", 30, 99.99);
            _miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(miembro);
            _membresiaRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(membresia);
            _repoMock.Setup(x => x.ObtenerActivaAsync(1)).ReturnsAsync((AsignacionMembresia?)null);
            _repoMock.Setup(x => x.CrearAsync(It.IsAny<AsignacionMembresia>())).ReturnsAsync(new AsignacionMembresia(1, membresia));

            var dto = new AsignacionMembresiaCrearDTO { MiembroId = 1, MembresiaId = 1 };
            var result = await _controller.Crear(dto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerTodos_ReturnsOk()
        {
            _repoMock.Setup(x => x.ObtenerTodosAsync()).ReturnsAsync(new List<AsignacionMembresia>());

            var result = await _controller.ObtenerTodos() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerPorId_ReturnsOk()
        {
            var membresia = new Membresia("Premium", 30, 99.99);
            var asig = new AsignacionMembresia(1, membresia);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(asig);

            var result = await _controller.ObtenerPorId(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Eliminar_ValidId_ReturnsOk()
        {
            var membresia = new Membresia("Premium", 30, 99.99);
            var asig = new AsignacionMembresia(1, membresia);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(asig);
            _repoMock.Setup(x => x.EliminarAsync(1)).ReturnsAsync(true);

            var result = await _controller.Eliminar(1) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
