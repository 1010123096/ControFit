using ControlFit.Application.DTO;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Application.CasosUso.CRUDAsignacion
{
    public class AsignacionMembresiaService
    {
        private readonly IAsignacionMembresiaRepository _repo;
        private readonly IMembresiaRepository _membresiaRepo;
        private readonly IMiembroRepository _miembroRepo;

        public AsignacionMembresiaService(
            IAsignacionMembresiaRepository repo,
            IMembresiaRepository membresiaRepo,
            IMiembroRepository miembroRepo)
        {
            _repo = repo;
            _membresiaRepo = membresiaRepo;
            _miembroRepo = miembroRepo;
        }

        public async Task<AsignacionMembresia> Crear(
            AsignacionMembresiaCrearDTO dto)
        {
            var miembro = await _miembroRepo.ObtenerPorIdAsync(dto.MiembroId);

            if (miembro == null)
            {
                throw new DomainException("El miembro no existe.");
            }

            var membresia = await _membresiaRepo.ObtenerPorIdAsync(dto.MembresiaId);

            if (membresia == null)
            {
                throw new DomainException("La membresía no existe.");
            }

            var asignacionActiva =
                await _repo.ObtenerActivaAsync(dto.MiembroId);

            if (asignacionActiva != null)
            {
                throw new DomainException(
                    "El miembro ya tiene una membresía activa.");
            }

            var asignacion =
                new AsignacionMembresia(
                    dto.MiembroId,
                    membresia);

            return await _repo.CrearAsync(asignacion);
        }

        public async Task<List<AsignacionMembresia>> ObtenerTodos()
        {
            return await _repo.ObtenerTodosAsync();
        }

        public async Task<AsignacionMembresia> ObtenerPorId(int id)
        {
            var asignacion = await _repo.ObtenerPorIdAsync(id);

            if (asignacion == null)
            {
                throw new DomainException("La asignación no existe.");
            }

            return asignacion;
        }

        public async Task Eliminar(int id)
        {
            var asignacion = await _repo.ObtenerPorIdAsync(id);

            if (asignacion == null)
            {
                throw new DomainException("La asignación no existe.");
            }

            var eliminado = await _repo.EliminarAsync(id);

            if (!eliminado)
            {
                throw new DomainException(
                    "No fue posible eliminar la asignación.");
            }
        }
    }
}
