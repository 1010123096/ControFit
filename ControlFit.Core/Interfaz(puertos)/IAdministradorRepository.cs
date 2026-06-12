using ControlFit.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Interfaz_puertos_
{
    public interface IAdministradorRepository
    {
        Task<Administrador?> ObtenerPorCorreoAsync(string correo);
        Task<Administrador?> ObtenerPorIdAsync(int id);
        Task GuardarAsync(Administrador admin);
        Task ActualizarAsync(Administrador admin);
        Task<List<Administrador>> ObtenerTodosAsync();
    }
}
