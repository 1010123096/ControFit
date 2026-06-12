using ControlFit.Domain.Entidad;

namespace ControlFit.Domain.Interfaz_puertos_
{
    public interface IConfiguracionPlataformaRepository
    {
        Task<ConfiguracionPlataforma?> ObtenerAsync();
        Task<ConfiguracionPlataforma> ObtenerOCrearPorDefectoAsync();
        Task<ConfiguracionPlataforma> GuardarAsync(ConfiguracionPlataforma configuracion);
    }
}
