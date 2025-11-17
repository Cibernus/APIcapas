using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;

namespace AReyes.Repositories.Interfaces
{
    public interface IProveedorRepository
    {
        // GET → Obtener todos los proveedores
        Task<IEnumerable<ProveedorEntity>> GetAllAsync();

        // GET → Obtener un proveedor por ID
        Task<ProveedorEntity?> GetByIdAsync(string id);

        // POST → Crear un nuevo proveedor
        Task CreateAsync(ProveedorEntity proveedor);

        // PUT → Actualizar un proveedor existente
        Task UpdateAsync(ProveedorEntity proveedor);

        // DELETE → Eliminar un proveedor por ID
        Task DeleteAsync(string id);

        Task<ProveedorEntity?> GetByRfcAsync(string rfc);
    }
}
