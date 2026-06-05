using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControlFit.Domain.Entidad
{
    public class Membresia
    {
        public int Id { get; private set; }
        public string Nombre {  get; private set; }
        public int Duración { get; private set; }
        public double Precio { get; private set; }
        public int? MaximoIngresosPorDia { get; private set; }
        public int? MaximoIngresosPorSemana { get; private set; }
        public int MaximoIngresosTotales { get; private set; }
        public bool Estado { get; private set; }
        
        /// <summary>
        /// ID del gimnasio al que pertenece esta membresía.
        /// </summary>
        public int GimnasioId { get; private set; }
        public Gimnasio Gimnasio { get; private set; }

        public Membresia(string nombre, int duración, double precio, bool estado)
        {
            Nombre = nombre;
            Duración = duración;
            Precio = precio;
            Estado = estado;
        }

        public Membresia() { }

        public Membresia(string nombre, int duración, double precio)
        {
            Nombre = nombre;
            Duración = duración;
            Precio = precio;
        }

        public Membresia(string nombre, int duración, double precio, int maximoIngresosPorDia, int maximoIngresosPorSemana, int maximoIngresosTotales, bool estado)
        {
            Nombre = nombre;
            Duración = duración;
            Precio = precio;
            MaximoIngresosPorDia = maximoIngresosPorDia;
            MaximoIngresosPorSemana = maximoIngresosPorSemana;
            MaximoIngresosTotales = maximoIngresosTotales;
            Estado = estado;
        }

        public Membresia(string nombre, int duración, double precio, int maximoIngresosPorDia, int maximoIngresosPorSemana, int maximoIngresosTotales, bool estado, int gimnasioId)
        {
            Nombre = nombre;
            Duración = duración;
            Precio = precio;
            MaximoIngresosPorDia = maximoIngresosPorDia;
            MaximoIngresosPorSemana = maximoIngresosPorSemana;
            MaximoIngresosTotales = maximoIngresosTotales;
            Estado = estado;
            GimnasioId = gimnasioId;
        }

        public void actualizar (string nombre, int duracion, double precio, int maximoIngresosPorDia, int maximoIngresosPorSemana, int maximoIngresosTotales)
        {
            Nombre = nombre;
            Duración = duracion;
            Precio  = precio;
            MaximoIngresosPorDia = maximoIngresosPorDia;
            MaximoIngresosPorSemana = maximoIngresosPorSemana;
            MaximoIngresosTotales = maximoIngresosTotales;
        }
    }
}
