using ControlFit.Application.CasosUso.CRUDAsignacion;
using ControlFit.Application.DTO;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Moq;

namespace ControlFit.Tests.Application
{
    public class AsignacionMembresiaServiceTests
    {
        private readonly Mock<IAsignacionMembresiaRepository> _repoMock;
        private readonly Mock<IMembresiaRepository> _membresiaRepoMock;
        private readonly Mock<IMiembroRepository> _miembroRepoMock;
        private readonly AsignacionMembresiaService _service;

        public AsignacionMembresiaServiceTests()
        {
            _repoMock = new Mock<IAsignacionMembresiaRepository>();
            _membresiaRepoMock = new Mock<IMembresiaRepository>();
            _miembroRepoMock = new Mock<IMiembroRepository>();
            _service = new AsignacionMembresiaService(_repoMock.Object, _membresiaRepoMock.Object, _miembroRepoMock.Object);
        }

        [Fact]
        public async Task Crear_WithValidData_ReturnsAsignacion()
        {
            var miembro = new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1);
            var membresia = new Membresia("Premium", 30, 99.99, 2, 10, 50, true, 1);
            _miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(miembro);
            _membresiaRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(membresia);
            _repoMock.Setup(x => x.ObtenerActivaAsync(1)).ReturnsAsync((AsignacionMembresia?)null);
            _repoMock.Setup(x => x.CrearAsync(It.IsAny<AsignacionMembresia>())).ReturnsAsync(new AsignacionMembresia(1, membresia));

            var dto = new AsignacionMembresiaCrearDTO { MiembroId = 1, MembresiaId = 1 };
            var result = await _service.Crear(dto);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Crear_MiembroNotFound_ThrowsException()
        {
            _miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(999)).ReturnsAsync((Miembro?)null);

            var dto = new AsignacionMembresiaCrearDTO { MiembroId = 999, MembresiaId = 1 };
            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.Crear(dto));

            Assert.Contains("miembro no existe", ex.Message);
        }

        [Fact]
        public async Task Crear_MembresiaNotFound_ThrowsException()
        {
            var miembro = new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1);
            _miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(miembro);
            _membresiaRepoMock.Setup(x => x.ObtenerPorIdAsync(999)).ReturnsAsync((Membresia?)null);

            var dto = new AsignacionMembresiaCrearDTO { MiembroId = 1, MembresiaId = 999 };
            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.Crear(dto));

            Assert.Contains("membresía no existe", ex.Message);
        }

        [Fact]
        public async Task Crear_ExistingActiveAsignacion_ThrowsException()
        {
            var miembro = new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1);
            var membresia = new Membresia("Premium", 30, 99.99);
            var activeAsig = new AsignacionMembresia(1, membresia);
            _miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(miembro);
            _membresiaRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(membresia);
            _repoMock.Setup(x => x.ObtenerActivaAsync(1)).ReturnsAsync(activeAsig);

            var dto = new AsignacionMembresiaCrearDTO { MiembroId = 1, MembresiaId = 1 };
            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.Crear(dto));

            Assert.Contains("ya tiene una membresía activa", ex.Message);
        }

        [Fact]
        public async Task ObtenerTodos_ReturnsList()
        {
            _repoMock.Setup(x => x.ObtenerTodosAsync()).ReturnsAsync(new List<AsignacionMembresia>());

            var result = await _service.ObtenerTodos();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Eliminar_NonExistent_ThrowsException()
        {
            _repoMock.Setup(x => x.ObtenerPorIdAsync(999)).ReturnsAsync((AsignacionMembresia?)null);

            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.Eliminar(999));
            Assert.Contains("no existe", ex.Message);
        }

        [Fact]
        public async Task Eliminar_Existing_CallsDelete()
        {
            var membresia = new Membresia("Premium", 30, 99.99);
            var asig = new AsignacionMembresia(1, membresia);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(asig);
            _repoMock.Setup(x => x.EliminarAsync(1)).ReturnsAsync(true);

            await _service.Eliminar(1);

            _repoMock.Verify(x => x.EliminarAsync(1), Times.Once);
        }
    }
}
