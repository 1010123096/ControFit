namespace ControlFit.Application.DTO
{
    public class ConfiguracionPlataformaDTO
    {
        public int Id { get; set; }
        public string NombreSistema { get; set; } = string.Empty;
        public string CorreoContacto { get; set; } = string.Empty;
        public string TelefonoContacto { get; set; } = string.Empty;
    }

    public class ActualizarConfiguracionPlataformaDTO
    {
        public string NombreSistema { get; set; } = string.Empty;
        public string CorreoContacto { get; set; } = string.Empty;
        public string TelefonoContacto { get; set; } = string.Empty;
    }
}
