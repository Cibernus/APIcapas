using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using AReyes.DTO;

namespace AReyes.Services.Interfaces
{
    public interface IProveedorService
    {
        // GET → Obtener todos los proveedores
        Task<IEnumerable<ProveedorEntity>> GetAllAsync();

        // GET → Obtener un proveedor por ID
        Task<ProveedorEntity?> GetByIdAsync(string id);

        // POST → Crear un nuevo proveedor
        Task CreateAsync(ProveedorDTOcs proveedor);

        // PUT → Actualizar un proveedor existente
        Task UpdateAsync(string id, ProveedorDTOcs proveedor);

        // DELETE → Eliminar un proveedor por ID
        Task DeleteAsync(string id);
    }
}
