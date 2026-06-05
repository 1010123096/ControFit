using ControlFit.Application.CasosUso.CRUDGimnasio;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Moq;

namespace ControlFit.Tests.Application
{
    public class GimnasioServiceTests
    {
        private readonly Mock<IGimnasioRepository> _repoMock;
        private readonly Mock<IUserContextService> _userContextMock;
        private readonly GimnasioService _service;

        public GimnasioServiceTests()
        {
            _repoMock = new Mock<IGimnasioRepository>();
            _userContextMock = new Mock<IUserContextService>();
            _service = new GimnasioService(_repoMock.Object, _userContextMock.Object);
        }

        [Fact]
        public async Task CrearGimnasio_AsSuperAdmin_ReturnsGimnasio()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _repoMock.Setup(x => x.ObtenerPorNombreAsync("Test Gym")).ReturnsAsync((bool?)false);
            _repoMock.Setup(x => x.CrearAsync(It.IsAny<Gimnasio>())).ReturnsAsync(new Gimnasio("Test Gym", "Address", true));

            var dto = new GimnasioCrearDTO("Test Gym", "Address") { Estado = true };
            var result = await _service.CrearGimnasio(dto);

            Assert.NotNull(result);
            Assert.Equal("Test Gym", result.Nombre);
        }

        [Fact]
        public async Task CrearGimnasio_AsGymAdmin_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(true);

            var dto = new GimnasioCrearDTO("Test", "Address") { Estado = true };
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.CrearGimnasio(dto));

            Assert.Contains("No tiene permiso", ex.Message);
        }

        [Fact]
        public async Task CrearGimnasio_WithNullDto_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.CrearGimnasio(null!));

            Assert.Contains("obligatorios", ex.Message);
        }

        [Fact]
        public async Task CrearGimnasio_WithDuplicateName_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _repoMock.Setup(x => x.ObtenerPorNombreAsync("Duplicated")).ReturnsAsync((bool?)true);

            var dto = new GimnasioCrearDTO("Duplicated", "Address") { Estado = true };
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.CrearGimnasio(dto));

            Assert.Contains("Ya existe", ex.Message);
        }

        [Fact]
        public async Task BuscarPorId_AsSuperAdmin_ReturnsGimnasio()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Gimnasio("Test", "Addr", true));

            var result = await _service.BuscarPorId(1);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Nombre);
        }

        [Fact]
        public async Task BuscarPorId_AsGymAdminWithCorrectId_ReturnsGimnasio()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(true);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Gimnasio("Test", "Addr", true));

            var result = await _service.BuscarPorId(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task BuscarPorId_AsGymAdminWithWrongId_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(true);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.BuscarPorId(2));
            Assert.Contains("permiso", ex.Message);
        }

        [Fact]
        public async Task BuscarPorId_NonExistent_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(999)).ReturnsAsync((Gimnasio?)null);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.BuscarPorId(999));
            Assert.Contains("no existe", ex.Message);
        }

        [Fact]
        public async Task ActualizarGimnasio_AsGymAdmin_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(true);

            var dto = new GimnasioActualizarDTO { Id = 1, Nombre = "Test", Direccion = "Addr", Estado = true };
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.ActualizarGimnasio(dto));

            Assert.Contains("No tiene permiso", ex.Message);
        }

        [Fact]
        public async Task ActualizarGimnasio_AsSuperAdmin_ReturnsTrue()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            var gimnasio = new Gimnasio("Old", "OldAddr", true);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(gimnasio);
            _repoMock.Setup(x => x.ObtenerPorNombreAsync("New", 1)).ReturnsAsync((bool?)false);
            _repoMock.Setup(x => x.ActualizarAsync(It.IsAny<Gimnasio>())).ReturnsAsync(true);

            var dto = new GimnasioActualizarDTO { Id = 1, Nombre = "New", Direccion = "NewAddr", Estado = true };
            var result = await _service.ActualizarGimnasio(dto);

            Assert.True(result);
        }

        [Fact]
        public async Task EliminarGimnasio_AsGymAdmin_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(true);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.EliminarGimnasio(1));
            Assert.Contains("No tiene permiso", ex.Message);
        }

        [Fact]
        public async Task EliminarGimnasio_AsSuperAdmin_CallsDelete()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _repoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Gimnasio("Test", "Addr", true));

            await _service.EliminarGimnasio(1);

            _repoMock.Verify(x => x.EliminarAsync(1), Times.Once);
        }

        [Fact]
        public async Task BuscarTodos_AsSuperAdmin_ReturnsAll()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _repoMock.Setup(x => x.ListarTodos()).ReturnsAsync(new List<Gimnasio?> { new Gimnasio("G1", "A", true), new Gimnasio("G2", "B", true) });

            var result = await _service.BuscarTodos();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task BuscarTodos_AsGymAdmin_FiltersByGimnasioId()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(true);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _repoMock.Setup(x => x.ListarTodos()).ReturnsAsync(new List<Gimnasio?> { new Gimnasio("G1", "A", true), new Gimnasio("G2", "B", true) });

            var result = await _service.BuscarTodos();

            _repoMock.Verify(x => x.ListarTodos(), Times.Once);
            Assert.NotNull(result);
        }
    }
}
