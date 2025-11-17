using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using Areyes.BaseDedatos;
using AReyes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AReyes.Repositories.Implementations
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AbarrotesReyesContext _context;

        public ProductoRepository(AbarrotesReyesContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos los productos
        public async Task<IEnumerable<ProductoEntity>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        // 🔹 Obtener producto por ID
        public async Task<ProductoEntity?> GetByIdAsync(string id)
        {
            return await _context.Productos.FindAsync(id);
        }

        // 🔹 Crear producto
        public async Task CreateAsync(ProductoEntity producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        // 🔹 Actualizar producto
        public async Task UpdateAsync(ProductoEntity producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        // 🔹 Eliminar producto
        public async Task DeleteAsync(string id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
