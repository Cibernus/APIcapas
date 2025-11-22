using AReyes.DTO;
using AReyes.Models;

namespace AReyes.Services.Interfaces
{
    public interface  IClienteService
    {

        // GET → Obtener todos los clientes
        Task<IEnumerable<ClienteEntity>> GetAllAsync();

        // GET → Obtener un cliente por ID
        Task<ClienteEntity?> GetByIdAsync(string id);

        // POST → Crear un nuevo cliente
        Task CreateAsync(ClienteDTO cliente);

        // PUT → Actualizar un cliente existente
        Task UpdateAsync(string id, ClienteDTO cliente);

        // DELETE → Eliminar un cliente por ID
        Task DeleteAsync(string id);

    }
}
