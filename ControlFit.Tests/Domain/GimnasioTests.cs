using ControlFit.Domain.Entidad;

namespace ControlFit.Tests.Domain
{
    public class GimnasioTests
    {
        [Fact]
        public void Constructor_WithValidData_SetsProperties()
        {
            var gimnasio = new Gimnasio("FitCenter", "Av. Siempre Viva 123", true);

            Assert.Equal("FitCenter", gimnasio.Nombre);
            Assert.Equal("Av. Siempre Viva 123", gimnasio.Direccion);
            Assert.True(gimnasio.Estado);
            Assert.Equal(DateOnly.FromDateTime(DateTime.Now), gimnasio.FechaCreacion);
        }

        [Fact]
        public void Constructor_Default_SetsDefaultValues()
        {
            var gimnasio = new Gimnasio();

            Assert.Equal(0, gimnasio.Id);
            Assert.Equal(string.Empty, gimnasio.Nombre);
        }

        [Fact]
        public void Actualizar_WithNewData_UpdatesProperties()
        {
            var gimnasio = new Gimnasio("OldName", "OldAddress", true);

            gimnasio.Actualizar("NewName", "NewAddress");

            Assert.Equal("NewName", gimnasio.Nombre);
            Assert.Equal("NewAddress", gimnasio.Direccion);
        }

        [Fact]
        public void EstablecerEstado_SetsEstadoToFalse()
        {
            var gimnasio = new Gimnasio("Test", "Address", true);

            gimnasio.EstablecerEstado(false);

            Assert.False(gimnasio.Estado);
        }

        [Fact]
        public void EstablecerEstado_SetsEstadoToTrue()
        {
            var gimnasio = new Gimnasio("Test", "Address", false);

            gimnasio.EstablecerEstado(true);

            Assert.True(gimnasio.Estado);
        }
    }
}
