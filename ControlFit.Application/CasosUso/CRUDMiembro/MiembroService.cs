using ControlFit.Application.DTO;
using ControlFit.Application.Repository;
using ControlFit.Application.Servicios;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControlFit.Application.CasosUso.CRUDMiembro
{
    public class MiembroService
    {
        private readonly IMiembroRepository _repo;
        private readonly IUserContextService _userContext;

        public MiembroService(IMiembroRepository repo, IUserContextService userContext)
        {
            _repo = repo;
            _userContext = userContext;
        }

        /// <summary>
        /// Crea un nuevo miembro en el gimnasio del usuario autenticado.
        /// Solo Admin de Gimnasio puede crear miembros en su gimnasio.
        /// Super Admin no puede crear miembros (debe asignarse a un gimnasio específico).
        /// </summary>
        public async Task<Miembro> Crearmiembro(MiembroDTO miembroDTO)
        {
            // Validar que el usuario es Admin de Gimnasio (no Super Admin)
            if (_userContext.EsSuperAdmin())
                throw new DomainException("Super Admin no puede crear miembros. Asígnese a un gimnasio primero.");

            var gimnasioId = _userContext.GetGimnasioId();
            
            // Validar que el GimnasioId del DTO coincide con el del usuario
            if (miembroDTO.GimnasioId != gimnasioId)
                throw new DomainException("No tiene permiso para crear miembros en este gimnasio.");

            var fechaNac = miembroDTO.FechaNacimiento ?? DateOnly.FromDateTime(DateTime.Today);
            var miembro = new Miembro(miembroDTO.Nombre, miembroDTO.Correo, miembroDTO.Telefono, fechaNac, miembroDTO.GimnasioId);
            return await _repo.CrearAsync(miembro);
        }

        /// <summary>
        /// Obtiene todos los miembros del gimnasio del usuario autenticado.
        /// Super Admin obtiene todos los miembros.
        /// Admin de Gimnasio obtiene solo sus miembros.
        /// </summary>
        public async Task<List<Miembro>> ListarMiembro()
        {
            var gimnasioId = _userContext.GetGimnasioId();

            if (_userContext.EsSuperAdmin())
                return await _repo.ListarTodos();
            else
                return await _repo.ListarTodosPorGimnasio(gimnasioId);
        }

        /// <summary>
        /// Obtiene un miembro específico validando permisos de gimnasio.
        /// </summary>
        public async Task<Miembro> ObtenerMiembroId(int id)
        {
            var gimnasioId = _userContext.GetGimnasioId();
            
            Miembro miembro;
            if (_userContext.EsSuperAdmin())
                miembro = await _repo.ObtenerPorIdAsync(id);
            else
                miembro = await _repo.ObtenerPorIdValidandoGimnasio(id, gimnasioId);

            if (miembro == null)
                throw new DomainException("Miembro no encontrado o no tiene permiso para acceder.");

            return miembro;
        }

        /// <summary>
        /// Actualiza los datos de un miembro validando permisos de gimnasio.
        /// </summary>
        public async Task<bool> ActualizarMiembro(int id, string nombre, string correo, string telefono)
        {
            var gimnasioId = _userContext.GetGimnasioId();

            Miembro miembro;
            if (_userContext.EsSuperAdmin())
                miembro = await _repo.ObtenerPorIdAsync(id);
            else
                miembro = await _repo.ObtenerPorIdValidandoGimnasio(id, gimnasioId);

            if (miembro == null)
                throw new DomainException("Miembro no encontrado o no tiene permiso para actualizar.");

            miembro.Actualizar(nombre, correo, telefono);
            return await _repo.ActualizarAsync(miembro);
        }

        /// <summary>
        /// Elimina un miembro validando permisos de gimnasio.
        /// </summary>
        public async Task EliminarMiembro(int id)
        {
            var gimnasioId = _userContext.GetGimnasioId();

            Miembro miembro;
            if (_userContext.EsSuperAdmin())
                miembro = await _repo.ObtenerPorIdAsync(id);
            else
                miembro = await _repo.ObtenerPorIdValidandoGimnasio(id, gimnasioId);

            if (miembro == null)
                throw new DomainException("Miembro no encontrado o no tiene permiso para eliminar.");

            await _repo.EliminarAsync(id);
        }
    }   
}
