using ControlFit.Application.CasosUso.Dashboard;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Moq;

namespace ControlFit.Tests.Application
{
    public class DashboardServiceTests
    {
        private readonly Mock<IMiembroRepository> _miembroRepoMock;
        private readonly Mock<IAsistenciaRepository> _asistenciaRepoMock;
        private readonly Mock<IAsignacionMembresiaRepository> _asignacionRepoMock;
        private readonly Mock<IGimnasioRepository> _gimnasioRepoMock;
        private readonly Mock<IAdministradorRepository> _adminRepoMock;
        private readonly Mock<IUserContextService> _userContextMock;
        private readonly DashboardService _service;

        public DashboardServiceTests()
        {
            _miembroRepoMock = new Mock<IMiembroRepository>();
            _asistenciaRepoMock = new Mock<IAsistenciaRepository>();
            _asignacionRepoMock = new Mock<IAsignacionMembresiaRepository>();
            _gimnasioRepoMock = new Mock<IGimnasioRepository>();
            _adminRepoMock = new Mock<IAdministradorRepository>();
            _userContextMock = new Mock<IUserContextService>();
            _service = new DashboardService(
                _miembroRepoMock.Object,
                _asistenciaRepoMock.Object,
                _asignacionRepoMock.Object,
                _gimnasioRepoMock.Object,
                _adminRepoMock.Object,
                _userContextMock.Object);
        }

        [Fact]
        public async Task ObtenerGymAdminDashboard_AdminGimnasio_ReturnsStats()
        {
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _miembroRepoMock.Setup(x => x.ListarTodosPorGimnasio(1))
                .ReturnsAsync(new List<Miembro>
                {
                    new Miembro("A", "a@a.com", "1", new DateOnly(1990, 1, 1), 1),
                    new Miembro("B", "b@b.com", "2", new DateOnly(1990, 1, 1), 1)
                });
            _asistenciaRepoMock.Setup(x => x.ObtenerTodosPorGimnasio(1))
                .ReturnsAsync(new List<Asistencia>());
            _asignacionRepoMock.Setup(x => x.ObtenerTodosPorGimnasio(1))
                .ReturnsAsync(new List<AsignacionMembresia>());

            var result = await _service.ObtenerGymAdminDashboard();

            Assert.Equal(2, result.TotalMiembros);
            Assert.Equal(2, result.MiembrosActivos);
            Assert.Equal(0, result.AsistenciasHoy);
            Assert.Equal(0, result.MembresiasVencidas);
        }

        [Fact]
        public async Task ObtenerGymAdminDashboard_AdminGimnasio_WithInactiveMembers_CountsActivos()
        {
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            var member1 = new Miembro("A", "a@a.com", "1", new DateOnly(1990, 1, 1), 1);
            var member2 = new Miembro("B", "b@b.com", "2", new DateOnly(1990, 1, 1), 1) { Estado = false };

            _miembroRepoMock.Setup(x => x.ListarTodosPorGimnasio(1))
                .ReturnsAsync(new List<Miembro> { member1, member2 });
            _asistenciaRepoMock.Setup(x => x.ObtenerTodosPorGimnasio(1))
                .ReturnsAsync(new List<Asistencia>());
            _asignacionRepoMock.Setup(x => x.ObtenerTodosPorGimnasio(1))
                .ReturnsAsync(new List<AsignacionMembresia>());

            var result = await _service.ObtenerGymAdminDashboard();

            Assert.Equal(2, result.TotalMiembros);
            Assert.Equal(1, result.MiembrosActivos);
        }

        [Fact]
        public async Task ObtenerGymAdminDashboard_WithVencidas_CountsVencidas()
        {
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _miembroRepoMock.Setup(x => x.ListarTodosPorGimnasio(1))
                .ReturnsAsync(new List<Miembro>());
            _asistenciaRepoMock.Setup(x => x.ObtenerTodosPorGimnasio(1))
                .ReturnsAsync(new List<Asistencia>());

            var membresiaVencida = new Membresia("Test", 30, 100);
            var vencida = new AsignacionMembresia(1, membresiaVencida);
            vencida.FechaInicio = DateTime.Now.AddDays(-10);
            vencida.FechaFin = DateTime.Now.AddDays(-1);
            _asignacionRepoMock.Setup(x => x.ObtenerTodosPorGimnasio(1))
                .ReturnsAsync(new List<AsignacionMembresia> { vencida });

            var result = await _service.ObtenerGymAdminDashboard();

            Assert.Equal(1, result.MembresiasVencidas);
        }

        [Fact]
        public async Task ObtenerGymAdminDashboard_WithAsistenciasHoy_CountsToday()
        {
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);
            _miembroRepoMock.Setup(x => x.ListarTodosPorGimnasio(1))
                .ReturnsAsync(new List<Miembro>());
            _asignacionRepoMock.Setup(x => x.ObtenerTodosPorGimnasio(1))
                .ReturnsAsync(new List<AsignacionMembresia>());

            var hoy = DateTime.Today.AddHours(10);
            var ayer = DateTime.Today.AddDays(-1).AddHours(10);
            var asistencias = new List<Asistencia>
            {
                new Asistencia(1, 1) { FechaHoraAcceso = hoy },
                new Asistencia(1, 1) { FechaHoraAcceso = ayer }
            };
            _asistenciaRepoMock.Setup(x => x.ObtenerTodosPorGimnasio(1))
                .ReturnsAsync(asistencias);

            var result = await _service.ObtenerGymAdminDashboard();

            Assert.Equal(1, result.AsistenciasHoy);
        }

        [Fact]
        public async Task ObtenerSuperAdminDashboard_ReturnsStats()
        {
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            _gimnasioRepoMock.Setup(x => x.ListarTodos())
                .ReturnsAsync(new List<Gimnasio?> { new Gimnasio("G1", "Addr", true) });
            _miembroRepoMock.Setup(x => x.ListarTodos())
                .ReturnsAsync(new List<Miembro>
                {
                    new Miembro("A", "a@a.com", "1", new DateOnly(1990, 1, 1), 1)
                });
            _adminRepoMock.Setup(x => x.ObtenerTodosAsync())
                .ReturnsAsync(new List<Administrador>
                {
                    new Administrador("Admin", "a@a.com", "hash")
                });

            var result = await _service.ObtenerSuperAdminDashboard();

            Assert.Equal(1, result.TotalGimnasios);
            Assert.Equal(1, result.TotalMiembros);
            Assert.Equal(1, result.TotalAdministradores);
        }

        [Fact]
        public async Task ObtenerSuperAdminDashboard_WhenGimnasioIdIsNull_UsesListarTodos()
        {
            _userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            _gimnasioRepoMock.Setup(x => x.ListarTodos())
                .ReturnsAsync(new List<Gimnasio?>());
            _miembroRepoMock.Setup(x => x.ListarTodos())
                .ReturnsAsync(new List<Miembro>());
            _adminRepoMock.Setup(x => x.ObtenerTodosAsync())
                .ReturnsAsync(new List<Administrador>());

            var result = await _service.ObtenerSuperAdminDashboard();

            _gimnasioRepoMock.Verify(x => x.ListarTodos(), Times.Once);
            _miembroRepoMock.Verify(x => x.ListarTodos(), Times.Once);
            _adminRepoMock.Verify(x => x.ObtenerTodosAsync(), Times.Once);
        }
    }
}
