using ControlFit.Domain.Entidad;

namespace ControlFit.Tests.Domain
{
    public class AsistenciaTests
    {
        [Fact]
        public void Constructor_WithValidData_SetsProperties()
        {
            var asistencia = new Asistencia(1, 5);

            Assert.Equal(1, asistencia.MiembroId);
            Assert.Equal(5, asistencia.AsignacionMembresiaId);
            Assert.Equal(DateTime.Now.Date, asistencia.FechaHoraAcceso.Date);
        }
    }
}
