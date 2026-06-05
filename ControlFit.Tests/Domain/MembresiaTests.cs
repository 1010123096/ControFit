using ControlFit.Domain.Entidad;

namespace ControlFit.Tests.Domain
{
    public class MembresiaTests
    {
        [Fact]
        public void Constructor_WithAllParams_SetsProperties()
        {
            var membresia = new Membresia("Premium", 30, 99.99, 2, 10, 50, true, 1);

            Assert.Equal("Premium", membresia.Nombre);
            Assert.Equal(30, membresia.Duración);
            Assert.Equal(99.99, membresia.Precio);
            Assert.Equal(2, membresia.MaximoIngresosPorDia);
            Assert.Equal(10, membresia.MaximoIngresosPorSemana);
            Assert.Equal(50, membresia.MaximoIngresosTotales);
            Assert.True(membresia.Estado);
            Assert.Equal(1, membresia.GimnasioId);
        }

        [Fact]
        public void Constructor_WithThreeParams_SetsNombreDuracionPrecio()
        {
            var membresia = new Membresia("Basic", 15, 49.99);

            Assert.Equal("Basic", membresia.Nombre);
            Assert.Equal(15, membresia.Duración);
            Assert.Equal(49.99, membresia.Precio);
        }

        [Fact]
        public void Constructor_Default_SetsDefaultValues()
        {
            var membresia = new Membresia();

            Assert.Equal(0, membresia.Id);
            Assert.Null(membresia.Nombre);
        }

        [Fact]
        public void Actualizar_WithNewData_UpdatesAllProperties()
        {
            var membresia = new Membresia("Old", 30, 100, 2, 10, 50, true, 1);

            membresia.actualizar("New", 60, 200, 3, 15, 100);

            Assert.Equal("New", membresia.Nombre);
            Assert.Equal(60, membresia.Duración);
            Assert.Equal(200, membresia.Precio);
            Assert.Equal(3, membresia.MaximoIngresosPorDia);
            Assert.Equal(15, membresia.MaximoIngresosPorSemana);
            Assert.Equal(100, membresia.MaximoIngresosTotales);
        }
    }
}
