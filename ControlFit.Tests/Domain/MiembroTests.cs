using ControlFit.Domain.Entidad;

namespace ControlFit.Tests.Domain
{
    public class MiembroTests
    {
        [Fact]
        public void Constructor_WithValidData_SetsProperties()
        {
            var fecha = new DateOnly(1990, 5, 15);
            var miembro = new Miembro("Juan Pérez", "juan@test.com", "999888777", fecha, 1);

            Assert.Equal("Juan Pérez", miembro.Nombre);
            Assert.Equal("juan@test.com", miembro.Correo);
            Assert.Equal("999888777", miembro.Telefono);
            Assert.Equal(fecha, miembro.FechaNacimiento);
            Assert.True(miembro.Estado);
            Assert.Equal(1, miembro.GimnasioId);
        }

        [Fact]
        public void Constructor_Default_SetsDefaultValues()
        {
            var miembro = new Miembro();

            Assert.Equal(0, miembro.Id);
            Assert.Equal("", miembro.Nombre);
        }

        [Fact]
        public void Actualizar_WithNewData_UpdatesProperties()
        {
            var fecha = new DateOnly(1990, 5, 15);
            var miembro = new Miembro("Old", "old@test.com", "111", fecha, 1);

            miembro.Actualizar("New Name", "new@test.com", "222");

            Assert.Equal("New Name", miembro.Nombre);
            Assert.Equal("new@test.com", miembro.Correo);
            Assert.Equal("222", miembro.Telefono);
        }
    }
}
