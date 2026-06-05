using ControlFit.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Interfaz_puertos_
{
    public interface IMiembroRepository
    {
        /// <summary>
        /// Obtiene un miembro por su ID sin validación de gimnasio.
        /// Usar solo en operaciones administrativas.
        /// </summary>
        Task<Miembro?> ObtenerPorIdAsync(int id);

        /// <summary>
        /// Obtiene un miembro por su ID validando que pertenece al gimnasio especificado.
        /// </summary>
        Task<Miembro?> ObtenerPorIdValidandoGimnasio(int id, int gimnasioId);

        Task<Miembro> CrearAsync(Miembro miembro);

        /// <summary>
        /// Obtiene todos los miembros sin validación de gimnasio.
        /// Usar solo en operaciones administrativas.
        /// </summary>
        Task<List<Miembro>> ListarTodos();

        /// <summary>
        /// Obtiene todos los miembros de un gimnasio específico.
        /// </summary>
        Task<List<Miembro>> ListarTodosPorGimnasio(int gimnasioId);

        Task<bool> ActualizarAsync(Miembro miembro);

        Task EliminarAsync(int id);
    }
}

