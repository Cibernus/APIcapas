using AReyes.Models;

namespace AReyes.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteEntity>> GetAllAsync();
        Task<ClienteEntity?> GetByIdAsync(string id);
        Task CreateAsync(ClienteEntity cliente);
        Task UpdateAsync(ClienteEntity cliente);
        Task DeleteAsync(string id);

        Task<ClienteEntity?> GetByCorreoAsync(string correo);
        Task<ClienteEntity?> GetByNombreUsuarioAsync(string NombreUsuario);


    }
}
