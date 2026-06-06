using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Application.CasosUso.CRUDGimnasio
{
    public class GimnasioService
    {
        private readonly IGimnasioRepository _repo;
        private readonly IUserContextService _userContext;

        public GimnasioService(IGimnasioRepository repo, IUserContextService userContext)
        {
            _repo = repo;
            _userContext = userContext;
        }

        /// <summary>
        /// Crea un nuevo gimnasio.
        /// Solo Super Admin puede crear gimnasios.
        /// Admin de Gimnasio no puede crear gimnasios.
        /// </summary>
        public async Task<Gimnasio> CrearGimnasio(GimnasioCrearDTO dto)
        {
            // Solo Super Admin puede crear gimnasios
            if (_userContext.EsAdminGimnasio())
                throw new DomainException("No tiene permiso para crear gimnasios. Solo Super Admin puede hacerlo.");

            if (dto == null)
                throw new DomainException("Los datos del gimnasio son obligatorios");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new DomainException("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(dto.Direccion))
                throw new DomainException("La dirección es obligatoria");

            var existente = await _repo.ObtenerPorNombreAsync(dto.Nombre);
            if (existente == true)
                throw new DomainException("Ya existe un gimnasio con ese nombre");

            var gimnasio = new Gimnasio(dto.Nombre, dto.Direccion, dto.Estado, dto.Telefono ?? "");

            return await _repo.CrearAsync(gimnasio);
        }

        /// <summary>
        /// Obtiene un gimnasio específico.
        /// Super Admin puede ver cualquier gimnasio.
        /// Admin de Gimnasio solo puede ver su propio gimnasio.
        /// </summary>
        public async Task<Gimnasio> BuscarPorId(int id)
        {
            if (id <= 0)
                throw new DomainException("Id inválido");

            // Si es Admin de Gimnasio, validar que es su gimnasio
            if (_userContext.EsAdminGimnasio() && _userContext.GetGimnasioId() != id)
                throw new DomainException("No tiene permiso para acceder a este gimnasio");

            var gimnasio = await _repo.ObtenerPorIdAsync(id);

            if (gimnasio == null)
                throw new DomainException("El gimnasio no existe");

            return gimnasio;
        }

        /// <summary>
        /// Obtiene todos los gimnasios.
        /// Super Admin obtiene todos.
        /// Admin de Gimnasio obtiene solo su propio gimnasio.
        /// </summary>
        public async Task<List<Gimnasio>> BuscarTodos()
        {
            var todos = await _repo.ListarTodos();

            // Si es Admin de Gimnasio, filtrar solo su gimnasio
            if (_userContext.EsAdminGimnasio())
            {
                var gimnasioId = _userContext.GetGimnasioId();
                return todos.Where(g => g != null && g.Id == gimnasioId).ToList();
            }

            return todos;
        }

        /// <summary>
        /// Actualiza los datos de un gimnasio.
        /// Solo Super Admin puede editar gimnasios.
        /// Admin de Gimnasio no puede editar ni su propio gimnasio.
        /// </summary>
        public async Task<bool> ActualizarGimnasio(GimnasioActualizarDTO dto)
        {
            // Solo Super Admin puede actualizar gimnasios
            if (_userContext.EsAdminGimnasio())
                throw new DomainException("No tiene permiso para editar gimnasios. Solo Super Admin puede hacerlo.");

            if (dto == null)
                throw new DomainException("Datos inválidos");

            var gimnasio = await _repo.ObtenerPorIdAsync(dto.Id);

            if (gimnasio == null)
                throw new DomainException("El gimnasio no existe");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new DomainException("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(dto.Direccion))
                throw new DomainException("La dirección es obligatoria");

            var duplicado = await _repo.ObtenerPorNombreAsync(dto.Nombre, dto.Id);
            if (duplicado == true)
                throw new DomainException("Ya existe otro gimnasio con ese nombre");

            gimnasio.Actualizar(dto.Nombre, dto.Direccion, dto.Telefono ?? "");
            gimnasio.EstablecerEstado(dto.Estado);

            return await _repo.ActualizarAsync(gimnasio);
        }

        /// <summary>
        /// Elimina un gimnasio.
        /// Solo Super Admin puede eliminar gimnasios.
        /// </summary>
        public async Task EliminarGimnasio(int id)
        {
            // Solo Super Admin puede eliminar gimnasios
            if (_userContext.EsAdminGimnasio())
                throw new DomainException("No tiene permiso para eliminar gimnasios. Solo Super Admin puede hacerlo.");

            var gimnasio = await _repo.ObtenerPorIdAsync(id);
            if (gimnasio == null)
                throw new DomainException("El gimnasio no existe");

            await _repo.EliminarAsync(id);
        }
    }
}
