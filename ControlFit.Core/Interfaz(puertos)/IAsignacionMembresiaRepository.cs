using ControlFit.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Interfaz_puertos_
{
    public interface IAsignacionMembresiaRepository
    {
        Task<AsignacionMembresia> CrearAsync(AsignacionMembresia asignacion);

        Task<AsignacionMembresia?> ObtenerPorIdAsync(int id);

        /// <summary>
        /// Obtiene todas las asignaciones sin validación de gimnasio.
        /// Usar solo en operaciones administrativas.
        /// </summary>
        Task<List<AsignacionMembresia>> ObtenerTodosAsync();

        /// <summary>
        /// Obtiene todas las asignaciones de un gimnasio específico.
        /// </summary>
        Task<List<AsignacionMembresia>> ObtenerTodosPorGimnasio(int gimnasioId);

        /// <summary>
        /// Obtiene todas las asignaciones de un miembro específico en un gimnasio.
        /// </summary>
        Task<List<AsignacionMembresia>> ObtenerPorMiembroGimnasio(int miembroId, int gimnasioId);

        Task<AsignacionMembresia?> ObtenerActivaAsync(int miembroId);

        Task<bool> EliminarAsync(int id);
    }
}
