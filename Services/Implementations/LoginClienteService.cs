using AReyes.DTO;
using AReyes.Models;
using AReyes.Repositories.Interfaces;
using AReyes.Services.Interfaces;
using BCrypt.Net;

namespace AReyes.Services.Implementations
{
    public class LoginClienteService : ILoginClienteService
    {
        private readonly IClienteRepository _clienteRepo;

        public LoginClienteService(IClienteRepository clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public async Task<ClienteEntity> LoginAsync(LoginClienteDTO dto)
        {
            // 🔹 Buscar cliente por correo
            var cliente = await _clienteRepo.GetByCorreoAsync(dto.Correo.Trim().ToLowerInvariant());

            // 🔹 Validaciones claras y directas
            if (cliente == null || !BCrypt.Net.BCrypt.Verify(dto.Contrasena, cliente.Contrasena))
            {
                throw new UnauthorizedAccessException("El correo no existe o las credenciales son incorrectas");
            }

            return cliente;
        }
    }
}


