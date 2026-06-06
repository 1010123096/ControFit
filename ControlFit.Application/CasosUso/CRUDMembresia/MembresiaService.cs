using ControlFit.Domain;
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
                throw new DomainException("Membresía no encontrada o no tiene permiso para acceder.");

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
                throw new DomainException("Los datos de la membresía son obligatorios");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new DomainException("El nombre de la membresía es obligatorio");

            if (dto.Duración <= 0)
                throw new DomainException("La duración debe ser mayor a 0");

            if (dto.Precio < 0)
                throw new DomainException("El precio no puede ser negativo");

            // Validar que el usuario es Admin de Gimnasio (no Super Admin)
            if (_userContext.EsSuperAdmin())
                throw new DomainException("Super Admin no puede crear membresías. Asígnese a un gimnasio primero.");

            var gimnasioId = _userContext.GetGimnasioId();

            // Validar que el GimnasioId del DTO coincide con el del usuario
            if (dto.GimnasioId != gimnasioId)
                throw new DomainException("No tiene permiso para crear membresías en este gimnasio.");

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
        /// Solo actualiza los campos que vienen en el DTO (campos nullable).
        /// </summary>
        public async Task<bool> Actualizar(ActualizarMembresiaDTO dto)
        {
            if (dto == null)
                throw new DomainException("Los datos de la membresía son obligatorios");

            var gimnasioId = _userContext.GetGimnasioId();

            Membresia membresia;
            if (_userContext.EsSuperAdmin())
                membresia = await _membresiaRepository.ObtenerPorIdAsync(dto.Id);
            else
                membresia = await _membresiaRepository.ObtenerPorIdValidandoGimnasio(dto.Id, gimnasioId);

            if (membresia == null)
                throw new DomainException("Membresía no encontrada o no tiene permiso para actualizar.");

            // Validar que el GimnasioId no cambió si no es Super Admin (solo si se envió)
            if (dto.GimnasioId.HasValue && _userContext.EsAdminGimnasio() && dto.GimnasioId.Value != membresia.GimnasioId)
                throw new DomainException("No puede cambiar el gimnasio asignado a la membresía.");

            // Actualizar solo los campos que vienen en el DTO
            if (dto.Nombre != null)
            {
                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    throw new DomainException("El nombre de la membresía es obligatorio");
                membresia.Nombre = dto.Nombre;
            }

            if (dto.Duración.HasValue)
            {
                if (dto.Duración.Value <= 0)
                    throw new DomainException("La duración debe ser mayor a 0");
                membresia.Duración = dto.Duración.Value;
            }

            if (dto.Precio.HasValue)
            {
                if (dto.Precio.Value < 0)
                    throw new DomainException("El precio no puede ser negativo");
                membresia.Precio = dto.Precio.Value;
            }

            if (dto.Estado.HasValue)
                membresia.Estado = dto.Estado.Value;

            if (dto.MaximoIngresosPorDia.HasValue)
                membresia.MaximoIngresosPorDia = dto.MaximoIngresosPorDia.Value;

            if (dto.MaximoIngresosPorSemana.HasValue)
                membresia.MaximoIngresosPorSemana = dto.MaximoIngresosPorSemana.Value;

            if (dto.MaximoIngresosTotales.HasValue)
                membresia.MaximoIngresosTotales = dto.MaximoIngresosTotales.Value;

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
                throw new DomainException("Membresía no encontrada o no tiene permiso para eliminar.");

            await _membresiaRepository.EliminarAsync(id);
        }
    }
}
