using ControlFit.Domain.Entidad;

namespace ControlFit.Tests.Domain
{
    public class AdministradorTests
    {
        [Fact]
        public void Constructor_WithGimnasioId_SetsAdminGimnasio()
        {
            var admin = new Administrador("Carlos", "carlos@test.com", "pass123", 1);

            Assert.Equal("Carlos", admin.NombreCompleto);
            Assert.Equal("carlos@test.com", admin.Correo);
            Assert.Equal(1, admin.GimnasioId);
        }

        [Fact]
        public void Constructor_WithoutGimnasioId_SetsSuperAdmin()
        {
            var admin = new Administrador("Super", "super@test.com", "pass123");

            Assert.Equal("Super", admin.NombreCompleto);
            Assert.Null(admin.GimnasioId);
        }

        [Fact]
        public void AsignarContrasena_HashesPassword()
        {
            var admin = new Administrador("Test", "test@test.com", "oldpass");

            admin.AsignarContrasena("newpass123");

            Assert.True(BCrypt.Net.BCrypt.Verify("newpass123", admin.Contrasena));
        }

        [Fact]
        public void ValidarPassword_CorrectPassword_ReturnsTrue()
        {
            var admin = new Administrador("Test", "test@test.com", BCrypt.Net.BCrypt.HashPassword("correctpass"));

            var result = admin.ValidarPassword("correctpass");

            Assert.True(result);
        }

        [Fact]
        public void ValidarPassword_WrongPassword_ReturnsFalse()
        {
            var admin = new Administrador("Test", "test@test.com", BCrypt.Net.BCrypt.HashPassword("correctpass"));

            var result = admin.ValidarPassword("wrongpass");

            Assert.False(result);
        }

        [Fact]
        public void Constructor_Default_SetsDefaultValues()
        {
            var admin = new Administrador();

            Assert.Equal(0, admin.Id);
            Assert.Equal("", admin.NombreCompleto);
        }
    }
}
