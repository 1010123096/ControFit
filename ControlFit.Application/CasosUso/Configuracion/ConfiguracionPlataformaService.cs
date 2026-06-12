using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;

namespace ControlFit.Application.CasosUso.Configuracion
{
    public class ConfiguracionPlataformaService
    {
        private readonly IConfiguracionPlataformaRepository _repo;
        private readonly IUserContextService _userContext;
        private readonly AuditService _auditService;

        public ConfiguracionPlataformaService(
            IConfiguracionPlataformaRepository repo,
            IUserContextService userContext,
            AuditService auditService)
        {
            _repo = repo;
            _userContext = userContext;
            _auditService = auditService;
        }

        public async Task<ConfiguracionPlataformaDTO> Obtener()
        {
            EnsureSuperAdmin();
            var config = await _repo.ObtenerOCrearPorDefectoAsync();
            return Map(config);
        }

        public async Task<ConfiguracionPlataformaDTO> Actualizar(ActualizarConfiguracionPlataformaDTO dto)
        {
            EnsureSuperAdmin();

            if (string.IsNullOrWhiteSpace(dto.NombreSistema))
                throw new DomainException("El nombre del sistema es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.CorreoContacto))
                throw new DomainException("El correo de contacto es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.TelefonoContacto))
                throw new DomainException("El teléfono de contacto es obligatorio.");

            var config = await _repo.ObtenerOCrearPorDefectoAsync();
            config.Actualizar(dto.NombreSistema.Trim(), dto.CorreoContacto.Trim(), dto.TelefonoContacto.Trim());
            var saved = await _repo.GuardarAsync(config);
            await _auditService.RegistrarAsync("Configuracion.Actualizada", "ConfiguracionPlataforma", saved.Id.ToString());
            return Map(saved);
        }

        private void EnsureSuperAdmin()
        {
            if (!_userContext.EsSuperAdmin())
                throw new DomainException("Solo el Super Administrador puede gestionar la configuración global.");
        }

        private static ConfiguracionPlataformaDTO Map(ConfiguracionPlataforma config) =>
            new()
            {
                Id = config.Id,
                NombreSistema = config.NombreSistema,
                CorreoContacto = config.CorreoContacto,
                TelefonoContacto = config.TelefonoContacto
            };
    }
}
