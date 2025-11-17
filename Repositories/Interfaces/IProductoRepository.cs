using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;

namespace AReyes.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<ProductoEntity>> GetAllAsync();
        Task<ProductoEntity?> GetByIdAsync(string id);
        Task CreateAsync(ProductoEntity producto);
        Task UpdateAsync(ProductoEntity producto);
        Task DeleteAsync(string id);
    }
}