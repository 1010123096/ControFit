using ControlFit.Application.CasosUso.CRUDMembresia;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Moq;

namespace ControlFit.Tests.Application
{
    public class MembresiaServiceTests
    {
        private readonly Mock<IMembresiaRepository> _repoMock;
        private readonly Mock<IUserContextService> _userContextMock;
        private readonly MembresiaService _service;

        public MembresiaServiceTests()
        {
            _repoMock = new Mock<IMembresiaRepository>();
            _userContextMock = new Mock<IUserContextService>();
            _service = new MembresiaService(_repoMock.Object, _userContextMock.Object);
        }

        [Fact]
        public async Task Crear_AsSuperAdmin_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);

            var dto = new CrearMembresiaDTO { Nombre = "Test", Duración = 30, Precio = 100, GimnasioId = 1 };
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Crear(dto));

            Assert.Contains("Super Admin", ex.Message);
        }

        [Fact]
        public async Task Crear_WithWrongGimnasioId_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);

            var dto = new CrearMembresiaDTO { Nombre = "Test", Duración = 30, Precio = 100, GimnasioId = 2 };
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Crear(dto));

            Assert.Contains("permiso", ex.Message);
        }

        [Fact]
        public async Task Crear_AsGymAdmin_ReturnsMembresia()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _repoMock.Setup(x => x.CrearAsync(It.IsAny<Membresia>())).ReturnsAsync(
                new Membresia("Test", 30, 100, 2, 10, 50, true, 1));

            var dto = new CrearMembresiaDTO { Nombre = "Test", Duración = 30, Precio = 100, MaximoIngresosPorDia = 2, MaximoIngresosPorSemana = 10, MaximoIngresosTotales = 50, GimnasioId = 1 };
            var result = await _service.Crear(dto);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Nombre);
        }

        [Fact]
        public async Task Crear_WithNullDto_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Crear(null!));
            Assert.Contains("obligatorios", ex.Message);
        }

        [Fact]
        public async Task Crear_WithEmptyNombre_ThrowsException()
        {
            var dto = new CrearMembresiaDTO { Nombre = "", Duración = 30, Precio = 100, GimnasioId = 1 };
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Crear(dto));

            Assert.Contains("nombre", ex.Message);
        }

        [Fact]
        public async Task Crear_WithNegativeDuracion_ThrowsException()
        {
            var dto = new CrearMembresiaDTO { Nombre = "Test", Duración = 0, Precio = 100, GimnasioId = 1 };
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Crear(dto));

            Assert.Contains("duración", ex.Message);
        }

        [Fact]
        public async Task ListarTodos_AsSuperAdmin_ReturnsAll()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ListarTodos()).ReturnsAsync(new List<Membresia> { new Membresia("M1", 30, 100) });

            var result = await _service.ListarTodos();

            Assert.Single(result);
        }

        [Fact]
        public async Task ListarTodos_AsGymAdmin_ReturnsByGimnasio()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _repoMock.Setup(x => x.ListarTodosPorGimnasio(1)).ReturnsAsync(new List<Membresia> { new Membresia("M1", 30, 100) });

            var result = await _service.ListarTodos();

            Assert.Single(result);
        }

        [Fact]
        public async Task ObtenerPorId_AsSuperAdmin_ReturnsMembresia()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Membresia("Test", 30, 100));

            var result = await _service.ObtenerPorId(1);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Nombre);
        }

        [Fact]
        public async Task ObtenerPorId_NotFound_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(999)).ReturnsAsync((Membresia?)null);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.ObtenerPorId(999));
            Assert.Contains("no encontrada", ex.Message);
        }

        [Fact]
        public async Task Actualizar_AsGymAdmin_ReturnsTrue()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _repoMock.Setup(x => x.ObtenerPorIdValidandoGimnasio(1, 1)).ReturnsAsync(new Membresia("Old", 30, 100, 2, 10, 50, true, 1));
            _repoMock.Setup(x => x.ActualizarAsync(It.IsAny<Membresia>())).ReturnsAsync(true);

            var dto = new ActualizarMembresiaDTO { Id = 1, Nombre = "New", Duración = 60, Precio = 200, MaximoIngresosPorDia = 3, MaximoIngresosPorSemana = 15, MaximoIngresosTotales = 100, GimnasioId = 1 };
            var result = await _service.Actualizar(dto);

            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_AsSuperAdmin_CallsDelete()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Membresia("Test", 30, 100));

            await _service.Eliminar(1);

            _repoMock.Verify(x => x.EliminarAsync(1), Times.Once);
        }

        [Fact]
        public async Task Eliminar_NotFound_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(999)).ReturnsAsync((Membresia?)null);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Eliminar(999));
            Assert.Contains("no encontrada", ex.Message);
        }
    }
}
