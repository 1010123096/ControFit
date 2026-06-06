using ControlFit.Application.DTO;
using ControlFit.Domain;
using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Application.CasosUso.Auth
{
    /// <summary>
    /// Caso de uso para registrar un nuevo administrador.
    /// Permite crear Super Admin (Sin GimnasioId) o Admin de Gimnasio (Con GimnasioId).
    /// </summary>
    public class RegistrarAdministrador
    {
        private readonly IAdministradorRepository _repo;

        public RegistrarAdministrador(IAdministradorRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Registra un nuevo administrador.
        /// </summary>
        /// <param name="dto">Datos del administrador a registrar</param>
        /// <remarks>
        /// - Si GimnasioId es null: crea Super Admin
        /// - Si GimnasioId > 0: crea Admin de Gimnasio
        /// </remarks>
        public async Task EjecutarAsyncRegistrar(RegistroAdministradorDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.nombreCompleto))
                throw new DomainException("El nombre completo es obligatorio");

            if (string.IsNullOrWhiteSpace(dto.correo))
                throw new DomainException("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(dto.contrasena) || dto.contrasena.Length < 6)
                throw new DomainException("La contraseña debe tener al menos 6 caracteres");

            // Verificar que el correo no esté registrado
            var existente = await _repo.ObtenerPorCorreoAsync(dto.correo);
            if (existente != null)
                throw new DomainException("El correo ya está registrado");

            var admin = new Administrador(
                dto.nombreCompleto,
                dto.correo,
                "",
                dto.GimnasioId  // null = Super Admin, > 0 = Admin de Gimnasio
            );

            admin.AsignarContrasena(dto.contrasena);
            await _repo.GuardarAsync(admin);
        }
    }
}
