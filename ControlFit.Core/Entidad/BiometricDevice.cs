namespace ControlFit.Domain.Entidad
{
    public class BiometricDevice
    {
        public int Id { get; set; }
        public int GimnasioId { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = "Inactive";
        public DateTime? LastSyncAt { get; set; }
    }
}
