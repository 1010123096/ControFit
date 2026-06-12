namespace ControlFit.Domain.Entidad
{
    public class AuditLogListado
    {
        public int Id { get; set; }
        public int? AdministradorId { get; set; }
        public string? AdministradorCorreo { get; set; }
        public int? GimnasioId { get; set; }
        public string? GimnasioNombre { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string Entidad { get; set; } = string.Empty;
        public string? EntidadId { get; set; }
        public string? Detalle { get; set; }
        public DateTime FechaUtc { get; set; }
    }
}
