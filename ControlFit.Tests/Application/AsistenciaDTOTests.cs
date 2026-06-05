using ControlFit.Application.DTO;

namespace ControlFit.Tests.Application
{
    public class AsistenciaDTOTests
    {
        [Fact]
        public void AsistenciaDTO_Constructor_SetsProperties()
        {
            var dto = new AsistenciaDTO(1, 2, 3, new DateTime(2026, 6, 3, 10, 30, 0), "Juan", "Premium");

            Assert.Equal(1, dto.Id);
            Assert.Equal(2, dto.MiembroId);
            Assert.Equal(3, dto.AsignacionMembresiaId);
            Assert.Equal(new DateTime(2026, 6, 3, 10, 30, 0), dto.FechaHoraAcceso);
            Assert.Equal("Juan", dto.NombreMiembro);
            Assert.Equal("Premium", dto.NombreMembresia);
        }

        [Fact]
        public void RegistroIngresoDTO_SetsMiembroId()
        {
            var dto = new RegistroIngresoDTO { MiembroId = 5 };

            Assert.Equal(5, dto.MiembroId);
        }

        [Fact]
        public void FiltroAsistenciaDTO_SetsProperties()
        {
            var dto = new FiltroAsistenciaDTO
            {
                MiembroId = 1,
                FechaInicio = new DateTime(2026, 1, 1),
                FechaFin = new DateTime(2026, 12, 31)
            };

            Assert.Equal(1, dto.MiembroId);
            Assert.Equal(new DateTime(2026, 1, 1), dto.FechaInicio);
            Assert.Equal(new DateTime(2026, 12, 31), dto.FechaFin);
        }
    }
}
