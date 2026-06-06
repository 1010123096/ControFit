using ControlFit.Application.DTO;
using ControlFit.Application.Repository;
using ControlFit.Domain.Interfaz_puertos_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Application.CasosUso.Auth
{
    /// <summary>
    /// Caso de uso para autenticar administrador y generar JWT token.
    /// Incluye el GimnasioId en el token para diferencia entre Super Admin y Admin de Gimnasio.
    /// </summary>
    public class LoginAdministrador
    {
        private readonly IAdministradorRepository _repo;
        private readonly ITokenService _tokenService;

        public LoginAdministrador(IAdministradorRepository repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Ejecuta el login del administrador.
        /// Retorna el token JWT si las credenciales son válidas.
        /// </summary>
        public async Task<string?> EjecutarAsyncLogin(LoginAdministradorDTO dto)
        {
            var administrador = await _repo.ObtenerPorCorreoAsync(dto.correo);
            if (administrador == null || !administrador.ValidarPassword(dto.contrasena))
                return null;

            // Generar token incluyendo GimnasioId (null = Super Admin) y nombre del gimnasio
            var nombreGimnasio = administrador.Gimnasio?.Nombre;
            return _tokenService.GenerarToken(administrador.Id, administrador.Correo, administrador.GimnasioId, nombreGimnasio);
        }
    }
}
