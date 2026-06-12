namespace ControlFit.Domain.Entidad
{
    public class Asistencia
    {
        public int Id { get; set; }
        public int MiembroId { get; set; }
        public int AsignacionMembresiaId { get; set; }
        public DateTime FechaHoraAcceso { get; set; }
        public FuenteAsistencia Fuente { get; set; } = FuenteAsistencia.Manual;
        public int? BiometricEventId { get; set; }

        public Asistencia() { }

        public Asistencia(
            int miembroId,
            int asignacionMembresiaId,
            FuenteAsistencia fuente = FuenteAsistencia.Manual,
            int? biometricEventId = null)
        {
            MiembroId = miembroId;
            AsignacionMembresiaId = asignacionMembresiaId;
            FechaHoraAcceso = DateTime.Now;
            Fuente = fuente;
            BiometricEventId = biometricEventId;
        }
    }
}
