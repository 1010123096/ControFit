using ControlFit.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Domain.Interfaz_puertos_
{
    public interface IGimnasioRepository
    {
        Task<Gimnasio?> ObtenerPorIdAsync(int id);
        
        Task<bool?> ObtenerPorNombreAsync(string nombre, int idActual);
        
        Task<bool?> ObtenerPorNombreAsync(string nombre);

        Task<Gimnasio> CrearAsync(Gimnasio gimnasio);
        
        Task<List<Gimnasio?>> ListarTodos();
        
        Task<bool> ActualizarAsync(Gimnasio gimnasio);
        
        Task EliminarAsync(int id);
    }
}
