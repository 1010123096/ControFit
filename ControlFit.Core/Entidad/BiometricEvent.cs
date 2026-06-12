namespace ControlFit.Domain.Entidad
{
    public class BiometricEvent
    {
        public int Id { get; set; }
        public int GimnasioId { get; set; }
        public int? DeviceId { get; set; }
        public string ExternalEventId { get; set; } = string.Empty;
        public string ExternalUserId { get; set; } = string.Empty;
        public int? MiembroId { get; set; }
        public DateTime EventAt { get; set; }
        public string EventType { get; set; } = "CheckIn";
        public string ProcessingStatus { get; set; } = "Pending";
        public int? AsistenciaId { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}
