using ControlFit.Api.Controllers;
using ControlFit.Application.CasosUso.Dashboard;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControlFit.Tests.Api
{
    public class DashboardControllerTests
    {
        private static DashboardController CreateController(
            Mock<IUserContextService>? userContextMock = null,
            Mock<IMiembroRepository>? miembroRepoMock = null,
            Mock<IAsistenciaRepository>? asistenciaRepoMock = null,
            Mock<IAsignacionMembresiaRepository>? asignacionRepoMock = null,
            Mock<IGimnasioRepository>? gimnasioRepoMock = null,
            Mock<IAdministradorRepository>? adminRepoMock = null)
        {
            userContextMock ??= new Mock<IUserContextService>();
            miembroRepoMock ??= new Mock<IMiembroRepository>();
            asistenciaRepoMock ??= new Mock<IAsistenciaRepository>();
            asignacionRepoMock ??= new Mock<IAsignacionMembresiaRepository>();
            gimnasioRepoMock ??= new Mock<IGimnasioRepository>();
            adminRepoMock ??= new Mock<IAdministradorRepository>();

            var service = new DashboardService(
                miembroRepoMock.Object,
                asistenciaRepoMock.Object,
                asignacionRepoMock.Object,
                gimnasioRepoMock.Object,
                adminRepoMock.Object,
                userContextMock.Object);

            return new DashboardController(service, userContextMock.Object);
        }

        [Fact]
        public async Task ObtenerGymAdminDashboard_SuperAdmin_ReturnsUnauthorized()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);

            var controller = CreateController(userContextMock: userContextMock);

            var result = await controller.ObtenerGymAdminDashboard() as UnauthorizedObjectResult;

            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerGymAdminDashboard_AdminGimnasio_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ListarTodosPorGimnasio(1))
                .ReturnsAsync(new List<Miembro> { new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1) });

            var asistenciaRepoMock = new Mock<IAsistenciaRepository>();
            asistenciaRepoMock.Setup(x => x.ObtenerTodosPorGimnasio(1))
                .ReturnsAsync(new List<Asistencia>());

            var asignacionRepoMock = new Mock<IAsignacionMembresiaRepository>();
            asignacionRepoMock.Setup(x => x.ObtenerTodosPorGimnasio(1))
                .ReturnsAsync(new List<AsignacionMembresia>());

            var controller = CreateController(
                userContextMock: userContextMock,
                miembroRepoMock: miembroRepoMock,
                asistenciaRepoMock: asistenciaRepoMock,
                asignacionRepoMock: asignacionRepoMock);

            var result = await controller.ObtenerGymAdminDashboard() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerGymAdminDashboard_SuperAdminViaDashboard_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ListarTodos())
                .ReturnsAsync(new List<Miembro>());

            var asistenciaRepoMock = new Mock<IAsistenciaRepository>();
            asistenciaRepoMock.Setup(x => x.ObtenerTodosAsync())
                .ReturnsAsync(new List<Asistencia>());

            var asignacionRepoMock = new Mock<IAsignacionMembresiaRepository>();
            asignacionRepoMock.Setup(x => x.ObtenerTodosAsync())
                .ReturnsAsync(new List<AsignacionMembresia>());

            var controller = CreateController(
                userContextMock: userContextMock,
                miembroRepoMock: miembroRepoMock,
                asistenciaRepoMock: asistenciaRepoMock,
                asignacionRepoMock: asignacionRepoMock);

            var result = await controller.ObtenerGymAdminDashboard() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerSuperAdminDashboard_AdminGimnasio_ReturnsUnauthorized()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(true);

            var controller = CreateController(userContextMock: userContextMock);

            var result = await controller.ObtenerSuperAdminDashboard() as UnauthorizedObjectResult;

            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerSuperAdminDashboard_SuperAdmin_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);

            var gimnasioRepoMock = new Mock<IGimnasioRepository>();
            gimnasioRepoMock.Setup(x => x.ListarTodos())
                .ReturnsAsync(new List<Gimnasio?> { new Gimnasio("G1", "Addr", true) });

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ListarTodos())
                .ReturnsAsync(new List<Miembro> { new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1) });

            var adminRepoMock = new Mock<IAdministradorRepository>();
            adminRepoMock.Setup(x => x.ObtenerTodosAsync())
                .ReturnsAsync(new List<Administrador> { new Administrador("Admin", "a@a.com", "hash") });

            var controller = CreateController(
                userContextMock: userContextMock,
                miembroRepoMock: miembroRepoMock,
                gimnasioRepoMock: gimnasioRepoMock,
                adminRepoMock: adminRepoMock);

            var result = await controller.ObtenerSuperAdminDashboard() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerGymAdminDashboard_ServiceThrows_ReturnsBadRequest()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsSuperAdmin()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Throws(new Exception("DB error"));

            var controller = CreateController(userContextMock: userContextMock);

            var result = await controller.ObtenerGymAdminDashboard() as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerSuperAdminDashboard_ServiceThrows_ReturnsBadRequest()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Throws(new Exception("DB error"));

            var controller = CreateController(userContextMock: userContextMock);

            var result = await controller.ObtenerSuperAdminDashboard() as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
