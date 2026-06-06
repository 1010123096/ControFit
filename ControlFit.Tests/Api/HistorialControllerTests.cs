using ControlFit.Api.Controllers;
using ControlFit.Application.CasosUso.Ingreso;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControlFit.Tests.Api
{
    public class HistorialControllerTests
    {
        [Fact]
        public async Task ObtenerHistorialMembresias_ValidMiembro_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var asigRepoMock = new Mock<IAsignacionMembresiaRepository>();
            asigRepoMock.Setup(x => x.ObtenerPorMiembroGimnasio(1, 0)).ReturnsAsync(new List<AsignacionMembresia>());

            var controller = new HistorialController(
                miembroRepoMock.Object,
                Mock.Of<IMembresiaRepository>(),
                asigRepoMock.Object,
                Mock.Of<IAsistenciaRepository>(),
                userContextMock.Object);

            var result = await controller.ObtenerHistorialMembresias(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialAsignaciones_ValidMiembro_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var asigRepoMock = new Mock<IAsignacionMembresiaRepository>();
            asigRepoMock.Setup(x => x.ObtenerPorMiembroGimnasio(1, 0)).ReturnsAsync(new List<AsignacionMembresia>());

            var controller = new HistorialController(
                miembroRepoMock.Object,
                Mock.Of<IMembresiaRepository>(),
                asigRepoMock.Object,
                Mock.Of<IAsistenciaRepository>(),
                userContextMock.Object);

            var result = await controller.ObtenerHistorialAsignaciones(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialAsistencias_ValidMiembro_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var asistenciaRepoMock = new Mock<IAsistenciaRepository>();
            asistenciaRepoMock.Setup(x => x.ObtenerPorMiembroEnRango(1, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 0)).ReturnsAsync(new List<Asistencia>());

            var controller = new HistorialController(
                miembroRepoMock.Object,
                Mock.Of<IMembresiaRepository>(),
                Mock.Of<IAsignacionMembresiaRepository>(),
                asistenciaRepoMock.Object,
                userContextMock.Object);

            var result = await controller.ObtenerHistorialAsistencias(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerResumenMiembro_ValidMiembro_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(1)).ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var asistenciaRepoMock = new Mock<IAsistenciaRepository>();
            asistenciaRepoMock.Setup(x => x.ObtenerPorMiembroEnRango(1, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 0)).ReturnsAsync(new List<Asistencia>());

            var controller = new HistorialController(
                miembroRepoMock.Object,
                Mock.Of<IMembresiaRepository>(),
                Mock.Of<IAsignacionMembresiaRepository>(),
                asistenciaRepoMock.Object,
                userContextMock.Object);

            var result = await controller.ObtenerResumenMiembro(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialMembresias_InvalidMiembroId_ReturnsBadRequest()
        {
            var controller = new HistorialController(
                Mock.Of<IMiembroRepository>(),
                Mock.Of<IMembresiaRepository>(),
                Mock.Of<IAsignacionMembresiaRepository>(),
                Mock.Of<IAsistenciaRepository>(),
                Mock.Of<IUserContextService>());

            var result = await controller.ObtenerHistorialMembresias(0) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialMembresias_MemberNotFound_ReturnsNotFound()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(999)).ReturnsAsync((Miembro?)null);

            var controller = new HistorialController(
                miembroRepoMock.Object,
                Mock.Of<IMembresiaRepository>(),
                Mock.Of<IAsignacionMembresiaRepository>(),
                Mock.Of<IAsistenciaRepository>(),
                userContextMock.Object);

            var result = await controller.ObtenerHistorialMembresias(999) as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialMembresias_AsGymAdmin_ValidMember_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(true);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ObtenerPorIdValidandoGimnasio(1, 1))
                .ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var membresia = new Membresia("Premium", 30, 99.99);
            var asignacion = new AsignacionMembresia(1, membresia);

            var asigRepoMock = new Mock<IAsignacionMembresiaRepository>();
            asigRepoMock.Setup(x => x.ObtenerPorMiembroGimnasio(1, 1))
                .ReturnsAsync(new List<AsignacionMembresia> { asignacion });

            var membresiaRepoMock = new Mock<IMembresiaRepository>();
            membresiaRepoMock.Setup(x => x.ObtenerPorIdAsync(asignacion.MembresiaId))
                .ReturnsAsync(membresia);

            var controller = new HistorialController(
                miembroRepoMock.Object,
                membresiaRepoMock.Object,
                asigRepoMock.Object,
                Mock.Of<IAsistenciaRepository>(),
                userContextMock.Object);

            var result = await controller.ObtenerHistorialMembresias(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialAsignaciones_AsGymAdmin_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(true);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(1);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ObtenerPorIdValidandoGimnasio(1, 1))
                .ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var membresia = new Membresia("Premium", 30, 99.99);
            var asignacion = new AsignacionMembresia(1, membresia);

            var asigRepoMock = new Mock<IAsignacionMembresiaRepository>();
            asigRepoMock.Setup(x => x.ObtenerPorMiembroGimnasio(1, 1))
                .ReturnsAsync(new List<AsignacionMembresia> { asignacion });

            var controller = new HistorialController(
                miembroRepoMock.Object,
                Mock.Of<IMembresiaRepository>(),
                asigRepoMock.Object,
                Mock.Of<IAsistenciaRepository>(),
                userContextMock.Object);

            var result = await controller.ObtenerHistorialAsignaciones(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerHistorialAsistencias_WithDateRange_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(1))
                .ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var asistencias = new List<Asistencia>
            {
                new Asistencia(1, 1)
            };

            var asistenciaRepoMock = new Mock<IAsistenciaRepository>();
            asistenciaRepoMock.Setup(x => x.ObtenerPorMiembroEnRango(1, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 0))
                .ReturnsAsync(asistencias);

            var controller = new HistorialController(
                miembroRepoMock.Object,
                Mock.Of<IMembresiaRepository>(),
                Mock.Of<IAsignacionMembresiaRepository>(),
                asistenciaRepoMock.Object,
                userContextMock.Object);

            var result = await controller.ObtenerHistorialAsistencias(1,
                DateTime.Now.AddDays(-10), DateTime.Now) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerResumenMiembro_WithActiveMembership_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            var miembroRepoMock = new Mock<IMiembroRepository>();
            miembroRepoMock.Setup(x => x.ObtenerPorIdAsync(1))
                .ReturnsAsync(new Miembro("Test", "t@t.com", "999", new DateOnly(1990, 1, 1), 1));

            var membresia = new Membresia("Premium", 30, 99.99);
            var asignacion = new AsignacionMembresia(1, membresia);

            var asigRepoMock = new Mock<IAsignacionMembresiaRepository>();
            asigRepoMock.Setup(x => x.ObtenerActivaAsync(1)).ReturnsAsync(asignacion);

            var membresiaRepoMock = new Mock<IMembresiaRepository>();
            membresiaRepoMock.Setup(x => x.ObtenerPorIdAsync(asignacion.MembresiaId))
                .ReturnsAsync(membresia);

            var asistenciaRepoMock = new Mock<IAsistenciaRepository>();
            asistenciaRepoMock.Setup(x => x.ObtenerPorMiembroEnRango(1, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 0))
                .ReturnsAsync(new List<Asistencia>());

            var controller = new HistorialController(
                miembroRepoMock.Object,
                membresiaRepoMock.Object,
                asigRepoMock.Object,
                asistenciaRepoMock.Object,
                userContextMock.Object);

            var result = await controller.ObtenerResumenMiembro(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
