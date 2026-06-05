using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Application.DTO
{
    /// <summary>
    /// DTO para registrar ingreso de un miembro.
    /// </summary>
    public class RegistroIngresoDTO
    {
        public int MiembroId { get; set; }
    }

    /// <summary>
    /// DTO para lectura de asistencias.
    /// </summary>
    public class AsistenciaDTO
    {
        public AsistenciaDTO(int id, int miembroId, int asignacionMembresiaId, DateTime fechaHoraAcceso, string nombreMiembro, string nombreMembresia)
        {
            Id = id;
            MiembroId = miembroId;
            AsignacionMembresiaId = asignacionMembresiaId;
            FechaHoraAcceso = fechaHoraAcceso;
            NombreMiembro = nombreMiembro;
            NombreMembresia = nombreMembresia;
        }

        public int Id { get; private set; }
        public int MiembroId { get; private set; }
        public int AsignacionMembresiaId { get; private set; }
        public DateTime FechaHoraAcceso { get; private set; }
        public string NombreMiembro { get; private set; }
        public string NombreMembresia { get; private set; }
    }

    /// <summary>
    /// DTO para filtrar asistencias.
    /// </summary>
    public class FiltroAsistenciaDTO
    {
        public int? MiembroId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
