using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.DTO;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using Moq;

namespace ControlFit.Tests.Application
{
    public class RegistrarAdministradorTests
    {
        private readonly Mock<IAdministradorRepository> _repoMock;
        private readonly RegistrarAdministrador _service;

        public RegistrarAdministradorTests()
        {
            _repoMock = new Mock<IAdministradorRepository>();
            _service = new RegistrarAdministrador(_repoMock.Object);
        }

        [Fact]
        public async Task EjecutarAsyncRegistrar_ValidData_GuardsAdmin()
        {
            _repoMock.Setup(x => x.ObtenerPorCorreoAsync("new@test.com")).ReturnsAsync((Administrador?)null);

            var dto = new RegistroAdministradorDTO { nombreCompleto = "New Admin", correo = "new@test.com", contrasena = "123456", GimnasioId = 1 };
            await _service.EjecutarAsyncRegistrar(dto);

            _repoMock.Verify(x => x.GuardarAsync(It.IsAny<Administrador>()), Times.Once);
        }

        [Fact]
        public async Task EjecutarAsyncRegistrar_EmptyNombre_ThrowsException()
        {
            var dto = new RegistroAdministradorDTO { nombreCompleto = "", correo = "test@test.com", contrasena = "123456" };

            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.EjecutarAsyncRegistrar(dto));
            Assert.Contains("nombre completo", ex.Message);
        }

        [Fact]
        public async Task EjecutarAsyncRegistrar_EmptyCorreo_ThrowsException()
        {
            var dto = new RegistroAdministradorDTO { nombreCompleto = "Test", correo = "", contrasena = "123456" };

            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.EjecutarAsyncRegistrar(dto));
            Assert.Contains("correo", ex.Message);
        }

        [Fact]
        public async Task EjecutarAsyncRegistrar_ShortPassword_ThrowsException()
        {
            var dto = new RegistroAdministradorDTO { nombreCompleto = "Test", correo = "test@test.com", contrasena = "12345" };

            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.EjecutarAsyncRegistrar(dto));
            Assert.Contains("contraseña", ex.Message);
        }

        [Fact]
        public async Task EjecutarAsyncRegistrar_DuplicateEmail_ThrowsException()
        {
            _repoMock.Setup(x => x.ObtenerPorCorreoAsync("existing@test.com")).ReturnsAsync(new Administrador("Existing", "existing@test.com", "pass", null));

            var dto = new RegistroAdministradorDTO { nombreCompleto = "Test", correo = "existing@test.com", contrasena = "123456" };
            var ex = await Assert.ThrowsAsync<DomainException>(() => _service.EjecutarAsyncRegistrar(dto));
            Assert.Contains("ya está registrado", ex.Message);
        }
    }
}
