using ControlFit.Application.CasosUso.CRUDMiembro;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Application.Repository;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Moq;

namespace ControlFit.Tests.Application
{
    public class MiembroServiceTests
    {
        private readonly Mock<IMiembroRepository> _repoMock;
        private readonly Mock<IUserContextService> _userContextMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly MiembroService _service;

        public MiembroServiceTests()
        {
            _repoMock = new Mock<IMiembroRepository>();
            _userContextMock = new Mock<IUserContextService>();
            _tokenServiceMock = new Mock<ITokenService>();
            _service = new MiembroService(_repoMock.Object, _userContextMock.Object);
        }

        [Fact]
        public async Task Crearmiembro_AsSuperAdmin_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);

            var dto = new MiembroDTO { Nombre = "Test", Correo = "test@test.com", Telefono = "999", FechaNacimiento = new DateOnly(1990, 1, 1), GimnasioId = 0 };
            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.Crearmiembro(dto));

            Assert.Contains("Super Admin", ex.Message);
        }

        [Fact]
        public async Task Crearmiembro_WithWrongGimnasioId_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);

            var dto = new MiembroDTO { Nombre = "Test", Correo = "test@test.com", Telefono = "999", FechaNacimiento = new DateOnly(1990, 1, 1), GimnasioId = 2 };
            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.Crearmiembro(dto));

            Assert.Contains("permiso", ex.Message);
        }

        [Fact]
        public async Task Crearmiembro_AsGymAdmin_ReturnsMiembro()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _repoMock.Setup(x => x.CrearAsync(It.IsAny<Miembro>())).ReturnsAsync(new Miembro("Test", "test@test.com", "999", new DateOnly(1990, 1, 1), 1));

            var dto = new MiembroDTO { Nombre = "Test", Correo = "test@test.com", Telefono = "999", FechaNacimiento = new DateOnly(1990, 1, 1), GimnasioId = 1 };
            var result = await _service.Crearmiembro(dto);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Nombre);
        }

        [Fact]
        public async Task ListarMiembro_AsSuperAdmin_ReturnsAll()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ListarTodos()).ReturnsAsync(new List<Miembro> { new Miembro("A", "a@a.com", "1", new DateOnly(1990, 1, 1), 1) });

            var result = await _service.ListarMiembro();

            Assert.Single(result);
        }

        [Fact]
        public async Task ListarMiembro_AsGymAdmin_ReturnsByGimnasio()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _repoMock.Setup(x => x.ListarTodosPorGimnasio(1)).ReturnsAsync(new List<Miembro> { new Miembro("A", "a@a.com", "1", new DateOnly(1990, 1, 1), 1) });

            var result = await _service.ListarMiembro();

            Assert.Single(result);
        }

        [Fact]
        public async Task ObtenerMiembroId_AsSuperAdmin_ReturnsMiembro()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var result = await _service.ObtenerMiembroId(1);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Nombre);
        }

        [Fact]
        public async Task ObtenerMiembroId_NotFound_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(999)).ReturnsAsync((Miembro?)null);

            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.ObtenerMiembroId(999));
            Assert.Contains("no encontrado", ex.Message);
        }

        [Fact]
        public async Task ActualizarMiembro_AsGymAdmin_ReturnsTrue()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _repoMock.Setup(x => x.ObtenerPorIdValidandoGimnasio(1, 1)).ReturnsAsync(new Miembro("Old", "old@test.com", "111", new DateOnly(1990, 1, 1), 1));
            _repoMock.Setup(x => x.ActualizarAsync(It.IsAny<Miembro>())).ReturnsAsync(true);

            var result = await _service.ActualizarMiembro(1, "New", "new@test.com", "222");

            Assert.True(result);
        }

        [Fact]
        public async Task EliminarMiembro_AsSuperAdmin_CallsDelete()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            await _service.EliminarMiembro(1);

            _repoMock.Verify(x => x.EliminarAsync(1), Times.Once);
        }

        [Fact]
        public async Task EliminarMiembro_NotFound_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(999)).ReturnsAsync((Miembro?)null);

            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.EliminarMiembro(999));
            Assert.Contains("no encontrado", ex.Message);
        }
    }
}
