using AReyes.DTO;
using AReyes.Models;

namespace AReyes.Services.Interfaces
{
    public interface ILoginService
    {
        Task<UsuarioEntity> LoginAsync(LoginDTO dto);
    }
}
