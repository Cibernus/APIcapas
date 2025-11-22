using AReyes.DTO;
using AReyes.Models;

namespace AReyes.Services.Interfaces
{
    public interface ILoginClienteService
    {
        Task<ClienteEntity> LoginAsync(LoginClienteDTO dto);
    }
}
