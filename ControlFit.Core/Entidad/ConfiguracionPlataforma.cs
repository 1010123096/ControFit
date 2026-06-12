namespace ControlFit.Domain.Entidad
{
    public class ConfiguracionPlataforma
    {
        public int Id { get; set; }
        public string NombreSistema { get; set; } = "CossGym";
        public string CorreoContacto { get; set; } = string.Empty;
        public string TelefonoContacto { get; set; } = string.Empty;

        public ConfiguracionPlataforma() { }

        public ConfiguracionPlataforma(string nombreSistema, string correoContacto, string telefonoContacto)
        {
            NombreSistema = nombreSistema;
            CorreoContacto = correoContacto;
            TelefonoContacto = telefonoContacto;
        }

        public void Actualizar(string nombreSistema, string correoContacto, string telefonoContacto)
        {
            NombreSistema = nombreSistema;
            CorreoContacto = correoContacto;
            TelefonoContacto = telefonoContacto;
        }
    }
}
