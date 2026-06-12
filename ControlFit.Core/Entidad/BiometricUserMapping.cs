namespace ControlFit.Domain.Entidad
{
    public class BiometricUserMapping
    {
        public int Id { get; set; }
        public int GimnasioId { get; set; }
        public string ExternalUserId { get; set; } = string.Empty;
        public int MiembroId { get; set; }
    }
}
