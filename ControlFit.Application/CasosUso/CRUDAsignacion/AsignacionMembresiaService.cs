using ControlFit.Application.DTO;
using ControlFit.Application.Servicios;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;

namespace ControlFit.Application.CasosUso.CRUDAsignacion
{
    public class AsignacionMembresiaService
    {
        private readonly IAsignacionMembresiaRepository _repo;
        private readonly IMembresiaRepository _membresiaRepo;
        private readonly IMiembroRepository _miembroRepo;
        private readonly IUserContextService _userContext;

        public AsignacionMembresiaService(
            IAsignacionMembresiaRepository repo,
            IMembresiaRepository membresiaRepo,
            IMiembroRepository miembroRepo,
            IUserContextService userContext)
        {
            _repo = repo;
            _membresiaRepo = membresiaRepo;
            _miembroRepo = miembroRepo;
            _userContext = userContext;
        }

        public async Task<AsignacionMembresia> Crear(AsignacionMembresiaCrearDTO dto)
        {
            if (_userContext.EsSuperAdmin())
                throw new DomainException("Super Admin no puede crear asignaciones. Use una cuenta de gimnasio.");

            var gimnasioId = _userContext.GetGimnasioId();
            var miembro = await _miembroRepo.ObtenerPorIdValidandoGimnasio(dto.MiembroId, gimnasioId);

            if (miembro == null)
                throw new DomainException("El miembro no existe o no pertenece a su gimnasio.");

            var membresia = await _membresiaRepo.ObtenerPorIdAsync(dto.MembresiaId);

            if (membresia == null || membresia.GimnasioId != gimnasioId)
                throw new DomainException("La membresía no existe o no pertenece a su gimnasio.");

            if (miembro.GimnasioId != membresia.GimnasioId)
                throw new DomainException("El miembro y la membresía deben pertenecer al mismo gimnasio.");

            var asignacionActiva = await _repo.ObtenerActivaAsync(dto.MiembroId);

            if (asignacionActiva != null)
                throw new DomainException("El miembro ya tiene una membresía activa.");

            var asignacion = new AsignacionMembresia(dto.MiembroId, membresia);
            return await _repo.CrearAsync(asignacion);
        }

        public async Task<List<AsignacionMembresia>> ObtenerTodos()
        {
            if (_userContext.EsSuperAdmin())
                return await _repo.ObtenerTodosAsync();

            return await _repo.ObtenerTodosPorGimnasio(_userContext.GetGimnasioId());
        }

        public async Task<AsignacionMembresia> ObtenerPorId(int id)
        {
            var asignacion = await ObtenerAsignacionConPermiso(id);
            return asignacion;
        }

        public async Task Eliminar(int id)
        {
            _ = await ObtenerAsignacionConPermiso(id);

            var eliminado = await _repo.EliminarAsync(id);

            if (!eliminado)
                throw new DomainException("No fue posible eliminar la asignación.");
        }

        private async Task<AsignacionMembresia> ObtenerAsignacionConPermiso(int id)
        {
            var asignacion = await _repo.ObtenerPorIdAsync(id);

            if (asignacion == null)
                throw new DomainException("La asignación no existe.");

            if (_userContext.EsSuperAdmin())
                return asignacion;

            var miembro = await _miembroRepo.ObtenerPorIdValidandoGimnasio(
                asignacion.MiembroId,
                _userContext.GetGimnasioId());

            if (miembro == null)
                throw new DomainException("No tiene permiso para acceder a esta asignación.");

            return asignacion;
        }
    }
}
