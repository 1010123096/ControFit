using ControlFit.Domain.Interfaz_puertos_;
using System;
using ControlFit.Domain.Entidad;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;

namespace ControlFit.Application.CasosUso.CRUDMembresia
{
    public class MembresiaService
    {
        private readonly IMembresiaRepository _membresiaRepository;
        private readonly IUserContextService _userContext;

        public MembresiaService(IMembresiaRepository membresiaRepository, IUserContextService userContext)
        {
            _membresiaRepository = membresiaRepository;
            _userContext = userContext;
        }

        /// <summary>
        /// Obtiene una membresía específica validando permisos de gimnasio.
        /// </summary>
        public async Task<Membresia> ObtenerPorId(int id)
        {
            var gimnasioId = _userContext.GetGimnasioId();

            Membresia membresia;
            if (_userContext.EsSuperAdmin())
                membresia = await _membresiaRepository.ObtenerPorIdAsync(id);
            else
                membresia = await _membresiaRepository.ObtenerPorIdValidandoGimnasio(id, gimnasioId);

            if (membresia == null)
                throw new Exception("Membresía no encontrada o no tiene permiso para acceder.");

            return membresia;
        }

        /// <summary>
        /// Crea una nueva membresía en el gimnasio del usuario autenticado.
        /// Solo Admin de Gimnasio puede crear membresías en su gimnasio.
        /// Super Admin no puede crear membresías (debe asignarse a un gimnasio específico).
        /// </summary>
        public async Task<Membresia> Crear(CrearMembresiaDTO dto)
        {
            if (dto == null)
                throw new Exception("Los datos de la membresía son obligatorios");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new Exception("El nombre de la membresía es obligatorio");

            if (dto.Duración <= 0)
                throw new Exception("La duración debe ser mayor a 0");

            if (dto.Precio < 0)
                throw new Exception("El precio no puede ser negativo");

            // Validar que el usuario es Admin de Gimnasio (no Super Admin)
            if (_userContext.EsSuperAdmin())
                throw new Exception("Super Admin no puede crear membresías. Asígnese a un gimnasio primero.");

            var gimnasioId = _userContext.GetGimnasioId();

            // Validar que el GimnasioId del DTO coincide con el del usuario
            if (dto.GimnasioId != gimnasioId)
                throw new Exception("No tiene permiso para crear membresías en este gimnasio.");

            var membresia = new Membresia(
                dto.Nombre,
                dto.Duración,
                dto.Precio,
                dto.MaximoIngresosPorDia,
                dto.MaximoIngresosPorSemana,
                dto.MaximoIngresosTotales,
                true,
                dto.GimnasioId
            );

            return await _membresiaRepository.CrearAsync(membresia);
        }

        /// <summary>
        /// Obtiene todas las membresías del gimnasio del usuario autenticado.
        /// Super Admin obtiene todas las membresías.
        /// Admin de Gimnasio obtiene solo las de su gimnasio.
        /// </summary>
        public async Task<List<Membresia>> ListarTodos()
        {
            var gimnasioId = _userContext.GetGimnasioId();

            if (_userContext.EsSuperAdmin())
                return await _membresiaRepository.ListarTodos();
            else
                return await _membresiaRepository.ListarTodosPorGimnasio(gimnasioId);
        }

        /// <summary>
        /// Actualiza una membresía validando permisos de gimnasio.
        /// </summary>
        public async Task<bool> Actualizar(ActualizarMembresiaDTO dto)
        {
            if (dto == null)
                throw new Exception("Los datos de la membresía son obligatorios");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new Exception("El nombre de la membresía es obligatorio");

            if (dto.Duración <= 0)
                throw new Exception("La duración debe ser mayor a 0");

            if (dto.Precio < 0)
                throw new Exception("El precio no puede ser negativo");

            var gimnasioId = _userContext.GetGimnasioId();

            Membresia membresia;
            if (_userContext.EsSuperAdmin())
                membresia = await _membresiaRepository.ObtenerPorIdAsync(dto.Id);
            else
                membresia = await _membresiaRepository.ObtenerPorIdValidandoGimnasio(dto.Id, gimnasioId);

            if (membresia == null)
                throw new Exception("Membresía no encontrada o no tiene permiso para actualizar.");

            // Validar que el GimnasioId no cambió si no es Super Admin
            if (_userContext.EsAdminGimnasio() && dto.GimnasioId != membresia.GimnasioId)
                throw new Exception("No puede cambiar el gimnasio asignado a la membresía.");

            membresia.actualizar(
                dto.Nombre,
                dto.Duración,
                dto.Precio,
                dto.MaximoIngresosPorDia,
                dto.MaximoIngresosPorSemana,
                dto.MaximoIngresosTotales
            );

            return await _membresiaRepository.ActualizarAsync(membresia);
        }

        /// <summary>
        /// Elimina una membresía validando permisos de gimnasio.
        /// </summary>
        public async Task Eliminar(int id)
        {
            var gimnasioId = _userContext.GetGimnasioId();

            Membresia membresia;
            if (_userContext.EsSuperAdmin())
                membresia = await _membresiaRepository.ObtenerPorIdAsync(id);
            else
                membresia = await _membresiaRepository.ObtenerPorIdValidandoGimnasio(id, gimnasioId);

            if (membresia == null)
                throw new Exception("Membresía no encontrada o no tiene permiso para eliminar.");

            await _membresiaRepository.EliminarAsync(id);
        }
    }
}
