using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Entidad
{
    public class AsignacionMembresia
    {
        public int Id { get; set; }

        public int MiembroId { get; set; }

        public int MembresiaId { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public DateTime FechaAsignacion => FechaInicio;

        public DateTime FechaVencimiento => FechaFin;

        public bool Estado { get; set; }

        public AsignacionMembresia() { }
        public AsignacionMembresia(int miembroId, Membresia membresia)
        {
            MiembroId = miembroId;

            MembresiaId = membresia.Id;

            FechaInicio = DateTime.Today;

            FechaFin = FechaInicio.AddDays(membresia.Duración);

            Estado = true;
        }

        public bool EstaVigente()
        {
            return Estado && DateTime.Today <= FechaFin;
        }
        public void Desactivar()
        {
            Estado = false;
        }
    }
}
