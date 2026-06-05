using ControlFit.Api.Controllers;
using ControlFit.Application.CasosUso.Ingreso;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControlFit.Tests.Api
{
    public class AsistenciaControllerTests
    {
        [Fact]
        public async Task RegistrarIngreso_Valid_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);

            var asistencias = new Mock<IAsistenciaRepository>();
            var asigs = new Mock<IAsignacionMembresiaRepository>();
            var membresias = new Mock<IMembresiaRepository>();
            var miembros = new Mock<IMiembroRepository>();

            var membresia = new Membresia("Premium", 30, 99.99, 2, 10, 50, true, 1);
            var asig = new AsignacionMembresia(1, membresia);
            asigs.Setup(x => x.ObtenerActivaAsync(1)).ReturnsAsync(asig);
            asistencias.Setup(x => x.YaIngresoHoyAsync(1)).ReturnsAsync(false);
            membresias.Setup(x => x.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(membresia);
            asistencias.Setup(x => x.ObtenerIngresosSemanaAsync(1)).ReturnsAsync(0);

            var service = new RegistrarIngreso(asistencias.Object, asigs.Object, membresias.Object, miembros.Object, userContextMock.Object);
            var controller = new AsistenciaController(service, asistencias.Object, miembros.Object, userContextMock.Object);

            var dto = new RegistroIngresoDTO { MiembroId = 1 };
            var result = await controller.RegistrarIngreso(dto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task RegistrarIngreso_InvalidMiembroId_ReturnsBadRequest()
        {
            var controller = new AsistenciaController(
                null!, Mock.Of<IAsistenciaRepository>(), Mock.Of<IMiembroRepository>(), Mock.Of<IUserContextService>());

            var dto = new RegistroIngresoDTO { MiembroId = 0 };
            var result = await controller.RegistrarIngreso(dto) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task RegistrarIngreso_ServiceThrows_ReturnsBadRequest()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);

            var asistencias = new Mock<IAsistenciaRepository>();
            var asigs = new Mock<IAsignacionMembresiaRepository>();
            var membresias = new Mock<IMembresiaRepository>();
            var miembros = new Mock<IMiembroRepository>();

            var membresia = new Membresia("Premium", 30, 99.99, 2, 10, 50, true, 1);
            var asig = new AsignacionMembresia(1, membresia);
            asigs.Setup(x => x.ObtenerActivaAsync(1)).ReturnsAsync(asig);
            asistencias.Setup(x => x.YaIngresoHoyAsync(1)).ReturnsAsync(false);
            membresias.Setup(x => x.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync(membresia);
            asistencias.Setup(x => x.ObtenerIngresosSemanaAsync(1)).ReturnsAsync(999);

            var service = new RegistrarIngreso(asistencias.Object, asigs.Object, membresias.Object, miembros.Object, userContextMock.Object);
            var controller = new AsistenciaController(service, asistencias.Object, miembros.Object, userContextMock.Object);

            var dto = new RegistroIngresoDTO { MiembroId = 1 };
            var result = await controller.RegistrarIngreso(dto) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task ObtenerAsistencias_AsSuperAdmin_ReturnsOk()
        {
            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsSuperAdmin()).Returns(true);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            var asistencias = new Mock<IAsistenciaRepository>();
            asistencias.Setup(x => x.ObtenerTodosAsync()).ReturnsAsync(new List<Asistencia>());

            var controller = new AsistenciaController(
                null!,
                asistencias.Object,
                Mock.Of<IMiembroRepository>(),
                userContextMock.Object);

            var result = await controller.ObtenerAsistencias() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
