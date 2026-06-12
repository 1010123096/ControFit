using ControlFit.Domain.Entidad;

namespace ControlFit.Application.DTO
{
    public class RegistroIngresoDTO
    {
        public int MiembroId { get; set; }
        public FuenteAsistencia Fuente { get; set; } = FuenteAsistencia.Manual;
        public int? BiometricEventId { get; set; }
    }

    public class PreviewIngresoDTO
    {
        public int MiembroId { get; set; }
        public string NombreMiembro { get; set; } = string.Empty;
        public string? NombreMembresia { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool PuedeIngresar { get; set; }
        public string? MotivoBloqueo { get; set; }
        public bool YaIngresoHoy { get; set; }
        public int IngresosSemana { get; set; }
        public int? MaximoIngresosSemana { get; set; }
    }

    public class AsistenciaDTO
    {
        public AsistenciaDTO(
            int id,
            int miembroId,
            int asignacionMembresiaId,
            DateTime fechaHoraAcceso,
            string nombreMiembro,
            string nombreMembresia,
            FuenteAsistencia fuente)
        {
            Id = id;
            MiembroId = miembroId;
            AsignacionMembresiaId = asignacionMembresiaId;
            FechaHoraAcceso = fechaHoraAcceso;
            NombreMiembro = nombreMiembro;
            NombreMembresia = nombreMembresia;
            Fuente = fuente;
        }

        public int Id { get; private set; }
        public int MiembroId { get; private set; }
        public int AsignacionMembresiaId { get; private set; }
        public DateTime FechaHoraAcceso { get; private set; }
        public string NombreMiembro { get; private set; }
        public string NombreMembresia { get; private set; }
        public FuenteAsistencia Fuente { get; private set; }
    }

    public class FiltroAsistenciaDTO
    {
        public int? MiembroId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
