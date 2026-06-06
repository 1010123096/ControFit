using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Entidad
{
    public class Asistencia
    {
        public int Id { get; set; }

        public int MiembroId { get; set; }

        public int AsignacionMembresiaId { get; set; }

        public DateTime FechaHoraAcceso { get; set; }

        public Asistencia(
            int miembroId,
            int asignacionMembresiaId)
        {
            MiembroId = miembroId;
            AsignacionMembresiaId = asignacionMembresiaId;
            FechaHoraAcceso = DateTime.Now;
        }
    }
}
