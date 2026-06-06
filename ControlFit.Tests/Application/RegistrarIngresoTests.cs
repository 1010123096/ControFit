using ControlFit.Application.CasosUso.Ingreso;
using ControlFit.Application.Servicios;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Moq;

namespace ControlFit.Tests.Application
{
    public class RegistrarIngresoTests
    {
        private readonly Mock<IAsistenciaRepository> _asistenciaRepoMock;
        private readonly Mock<IAsignacionMembresiaRepository> _asignacionRepoMock;
        private readonly Mock<IMembresiaRepository> _membresiaRepoMock;
        private readonly Mock<IMiembroRepository> _miembroRepoMock;
        private readonly Mock<IUserContextService> _userContextMock;
        private readonly RegistrarIngreso _service;

        public RegistrarIngresoTests()
        {
            _asistenciaRepoMock = new Mock<IAsistenciaRepository>();
            _asignacionRepoMock = new Mock<IAsignacionMembresiaRepository>();
            _membresiaRepoMock = new Mock<IMembresiaRepository>();
            _miembroRepoMock = new Mock<IMiembroRepository>();
            _userContextMock = new Mock<IUserContextService>();
            _service = new RegistrarIngreso(
                _asistenciaRepoMock.Object,
                _asignacionRepoMock.Object,
                _membresiaRepoMock.Object,
                _miembroRepoMock.Object,
                _userContextMock.Object);
        }

        [Fact]
        public async Task EjecutarAsync_AsGymAdminWithWrongMember_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(true);
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _miembroRepoMock.Setup(x => x.ObtenerPorIdValidandoGimnasio(999, 1)).ReturnsAsync((Miembro?)null);

            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.EjecutarAsync(999));
            Assert.Contains("permiso", ex.Message);
        }

        [Fact]
        public async Task EjecutarAsync_NoActiveAsignacion_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            _asignacionRepoMock.Setup(x => x.ObtenerActivaAsync(1)).ReturnsAsync((AsignacionMembresia?)null);

            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.EjecutarAsync(1));
            Assert.Contains("Membresía vencida", ex.Message);
        }

        [Fact]
        public async Task EjecutarAsync_AlreadyCheckedInToday_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            var membresia = new Membresia("Premium", 30, 99.99);
            var asig = new AsignacionMembresia(1, membresia);
            _asignacionRepoMock.Setup(x => x.ObtenerActivaAsync(1)).ReturnsAsync(asig);
            _asistenciaRepoMock.Setup(x => x.YaIngresoHoyAsync(1)).ReturnsAsync(true);

            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.EjecutarAsync(1));
            Assert.Contains("ya ingresó hoy", ex.Message);
        }

        [Fact]
        public async Task EjecutarAsync_WeeklyLimitReached_ThrowsException()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            var membresia = new Membresia("Premium", 30, 99.99, 2, 3, 50, true, 1);
            var asig = new AsignacionMembresia(1, membresia);
            _asignacionRepoMock.Setup(x => x.ObtenerActivaAsync(1)).ReturnsAsync(asig);
            _asistenciaRepoMock.Setup(x => x.YaIngresoHoyAsync(1)).ReturnsAsync(false);
            _membresiaRepoMock.Setup(x => x.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(membresia);
            _asistenciaRepoMock.Setup(x => x.ObtenerIngresosSemanaAsync(1)).ReturnsAsync(3);

            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.EjecutarAsync(1));
            Assert.Contains("Límite semanal", ex.Message);
        }

        [Fact]
        public async Task EjecutarAsync_Valid_CallsRegistrarAsync()
        {
            _userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            var membresia = new Membresia("Premium", 30, 99.99, 2, 10, 50, true, 1);
            var asig = new AsignacionMembresia(1, membresia);
            _asignacionRepoMock.Setup(x => x.ObtenerActivaAsync(1)).ReturnsAsync(asig);
            _asistenciaRepoMock.Setup(x => x.YaIngresoHoyAsync(1)).ReturnsAsync(false);
            _membresiaRepoMock.Setup(x => x.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(membresia);
            _asistenciaRepoMock.Setup(x => x.ObtenerIngresosSemanaAsync(1)).ReturnsAsync(0);

            await _service.EjecutarAsync(1);

            _asistenciaRepoMock.Verify(x => x.RegistrarAsync(It.IsAny<Asistencia>()), Times.Once);
        }
    }
}
