using AReyes.DTO;
using AReyes.Models;
using AReyes.Repositories.Interfaces;
using AReyes.Services.Interfaces;
using BCrypt.Net;

namespace AReyes.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioRepository _usuarioRepo;

        public LoginService(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public async Task<UsuarioEntity>LoginAsync(LoginDTO dto)
        {
            // Buscar usuario por correo
            var usuario = await _usuarioRepo.GetByCorreoAsync(dto.CorreoElectronico.Trim().ToLowerInvariant());

            // Validaciones claras y directas
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.Contrasena))
            {
                throw new UnauthorizedAccessException("El correo no existe o las credenciales son incorrectas");
            }

            return usuario;
        }
    }
}
