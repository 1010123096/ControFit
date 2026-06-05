using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Entidad
{
    public class Asistencia
    {
        public int Id { get; private set; }

        public int MiembroId { get; private set; }

        public int AsignacionMembresiaId { get; private set; }

        public DateTime FechaHoraAcceso { get; private set; }

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
