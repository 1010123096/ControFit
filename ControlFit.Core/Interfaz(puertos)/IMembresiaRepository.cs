using ControlFit.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Interfaz_puertos_
{
    public interface IMembresiaRepository
    {
        /// <summary>
        /// Obtiene una membresía por su ID sin validación de gimnasio.
        /// </summary>
        Task<Membresia?> ObtenerPorIdAsync(int id);

        /// <summary>
        /// Obtiene una membresía por su ID validando que pertenece al gimnasio especificado.
        /// </summary>
        Task<Membresia?> ObtenerPorIdValidandoGimnasio(int id, int gimnasioId);

        Task<Membresia> CrearAsync(Membresia membresia);

        /// <summary>
        /// Obtiene todas las membresías sin validación de gimnasio.
        /// Usar solo en operaciones administrativas.
        /// </summary>
        Task<List<Membresia>> ListarTodos();

        /// <summary>
        /// Obtiene todas las membresías de un gimnasio específico.
        /// </summary>
        Task<List<Membresia>> ListarTodosPorGimnasio(int gimnasioId);

        Task<bool> ActualizarAsync(Membresia membresia);

        Task EliminarAsync(int id);
    }
}
