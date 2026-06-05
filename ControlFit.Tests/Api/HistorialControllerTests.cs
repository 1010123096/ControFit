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
    }
}
