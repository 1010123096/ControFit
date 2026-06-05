using ControlFit.Domain.Entidad;

namespace ControlFit.Tests.Domain
{
    public class AsignacionMembresiaTests
    {
        [Fact]
        public void Constructor_WithMiembroAndMembresia_SetsProperties()
        {
            var membresia = new Membresia("Premium", 30, 99.99, 2, 10, 50, true, 1);
            var asignacion = new AsignacionMembresia(1, membresia);

            Assert.Equal(1, asignacion.MiembroId);
            Assert.Equal(0, asignacion.MembresiaId);
            Assert.Equal(DateTime.Today, asignacion.FechaInicio);
            Assert.Equal(DateTime.Today.AddDays(30), asignacion.FechaFin);
            Assert.True(asignacion.Estado);
        }

        [Fact]
        public void EstaVigente_ActiveAndNotExpired_ReturnsTrue()
        {
            var membresia = new Membresia("Premium", 30, 99.99);
            var asignacion = new AsignacionMembresia(1, membresia);

            var vigente = asignacion.EstaVigente();

            Assert.True(vigente);
        }

        [Fact]
        public void EstaVigente_Desactivated_ReturnsFalse()
        {
            var membresia = new Membresia("Premium", 30, 99.99);
            var asignacion = new AsignacionMembresia(1, membresia);
            asignacion.Desactivar();

            var vigente = asignacion.EstaVigente();

            Assert.False(vigente);
        }

        [Fact]
        public void Desactivar_SetsEstadoToFalse()
        {
            var membresia = new Membresia("Premium", 30, 99.99);
            var asignacion = new AsignacionMembresia(1, membresia);

            asignacion.Desactivar();

            Assert.False(asignacion.Estado);
        }

        [Fact]
        public void Constructor_Default_SetsDefaultValues()
        {
            var asignacion = new AsignacionMembresia();

            Assert.Equal(0, asignacion.Id);
            Assert.Equal(default, asignacion.FechaInicio);
        }
    }
}
