using ControlFit.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Interfaz_puertos_
{
    public interface IAsistenciaRepository
    {
        Task RegistrarAsync(Asistencia asistencia);

        Task<bool> YaIngresoHoyAsync(int miembroId);

        Task<int> ObtenerIngresosSemanaAsync(int miembroId);

        /// <summary>
        /// Obtiene todas las asistencias registradas sin validación de gimnasio.
        /// Usar solo en operaciones administrativas.
        /// </summary>
        Task<List<Asistencia>> ObtenerTodosAsync();

        /// <summary>
        /// Obtiene todas las asistencias de un gimnasio específico.
        /// </summary>
        Task<List<Asistencia>> ObtenerTodosPorGimnasio(int gimnasioId);

        /// <summary>
        /// Obtiene las asistencias de un miembro en un rango de fechas y gimnasio.
        /// </summary>
        Task<List<Asistencia>> ObtenerPorMiembroEnRango(int miembroId, DateTime fechaInicio, DateTime fechaFin, int gimnasioId);

        /// <summary>
        /// Conteo diario de asistencias de los últimos 7 días (incluye hoy) para un gimnasio.
        /// </summary>
        Task<int[]> ObtenerConteoDiarioUltimos7DiasPorGimnasio(int gimnasioId);
    }
}
