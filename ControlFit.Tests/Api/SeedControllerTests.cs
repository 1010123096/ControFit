using ControlFit.Api.Controllers;
using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.CasosUso.CRUDGimnasio;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControlFit.Tests.Api
{
    public class SeedControllerTests
    {
        [Fact]
        public async Task Seed_CreatesData_ReturnsOk()
        {
            var adminRepoMock = new Mock<IAdministradorRepository>();
            adminRepoMock.Setup(x => x.ObtenerPorCorreoAsync(It.IsAny<string>())).ReturnsAsync((Administrador?)null);

            var gymRepoMock = new Mock<IGimnasioRepository>();
            gymRepoMock.Setup(x => x.ObtenerPorNombreAsync(It.IsAny<string>())).ReturnsAsync((bool?)false);
            gymRepoMock.Setup(x => x.CrearAsync(It.IsAny<Gimnasio>())).ReturnsAsync(new Gimnasio("FitZone Gym", "Av. Principal 123, Lima", true));

            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            var registrar = new RegistrarAdministrador(adminRepoMock.Object);
            var gymService = new GimnasioService(gymRepoMock.Object, userContextMock.Object);

            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(x => x.EnvironmentName).Returns(Environments.Development);

            var controller = new SeedController(registrar, gymService, envMock.Object);
            var result = await controller.Seed() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Seed_ServiceThrows_ReturnsBadRequest()
        {
            var adminRepoMock = new Mock<IAdministradorRepository>();
            adminRepoMock.Setup(x => x.ObtenerPorCorreoAsync(It.IsAny<string>())).ReturnsAsync((Administrador?)null);

            var gymRepoMock = new Mock<IGimnasioRepository>();
            gymRepoMock.Setup(x => x.ObtenerPorNombreAsync(It.IsAny<string>())).ReturnsAsync((bool?)false);
            gymRepoMock.Setup(x => x.CrearAsync(It.IsAny<Gimnasio>())).ThrowsAsync(new DomainException("Error de prueba"));

            var userContextMock = new Mock<IUserContextService>();
            userContextMock.Setup(x => x.EsAdminGimnasio()).Returns(false);
            userContextMock.Setup(x => x.GetGimnasioId()).Returns(0);

            var registrar = new RegistrarAdministrador(adminRepoMock.Object);
            var gymService = new GimnasioService(gymRepoMock.Object, userContextMock.Object);

            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(x => x.EnvironmentName).Returns(Environments.Development);

            var controller = new SeedController(registrar, gymService, envMock.Object);
            var result = await controller.Seed() as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
