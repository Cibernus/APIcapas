using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using AReyes.DTO;

namespace AReyes.Services.Interfaces
{
    public interface IProductoService
    {
        // GET → Obtener todos los productos
        Task<IEnumerable<ProductoEntity>> GetAllAsync();

        // GET → Obtener un producto por ID
        Task<ProductoEntity?> GetByIdAsync(string id);

        // POST → Crear un nuevo producto
        Task CreateAsync(ProductoDTO producto);

        // PUT → Actualizar un producto existente
        Task UpdateAsync(string id, ProductoDTO producto);

        // DELETE → Eliminar un producto por ID
        Task DeleteAsync(string id);
    }
}
