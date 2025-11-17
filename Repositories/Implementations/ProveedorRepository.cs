using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using Areyes.BaseDedatos;
using AReyes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AReyes.Repositories.Implementations
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly AbarrotesReyesContext _context;

        public ProveedorRepository(AbarrotesReyesContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos los proveedores
        public async Task<IEnumerable<ProveedorEntity>> GetAllAsync()
        {
            return await _context.Proveedores.ToListAsync();
        }

        // 🔹 Obtener proveedor por ID
        public async Task<ProveedorEntity?> GetByIdAsync(string id)
        {
            return await _context.Proveedores.FindAsync(id);
        }

        // 🔹 Crear proveedor
        public async Task CreateAsync(ProveedorEntity proveedor)
        {
            await _context.Proveedores.AddAsync(proveedor);
            await _context.SaveChangesAsync();
        }

        // 🔹 Actualizar proveedor
        public async Task UpdateAsync(ProveedorEntity proveedor)
        {
            _context.Proveedores.Update(proveedor);
            await _context.SaveChangesAsync();
        }

        // 🔹 Eliminar proveedor
        public async Task DeleteAsync(string id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor != null)
            {
                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();
            }

        }
            public async Task<ProveedorEntity?> GetByRfcAsync(string rfc)
        {
            return await _context.Proveedores
                                 .FirstOrDefaultAsync(p => p.Rfc == rfc);
        }


    }
}
