using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using Areyes.BaseDedatos;
using AReyes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AReyes.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AbarrotesReyesContext _context;

        public ClienteRepository(AbarrotesReyesContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos los clientes
        public async Task<IEnumerable<ClienteEntity>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        // 🔹 Obtener cliente por ID
        public async Task<ClienteEntity?> GetByIdAsync(string id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        // 🔹 Crear cliente
        public async Task CreateAsync(ClienteEntity cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        // 🔹 Actualizar cliente
        public async Task UpdateAsync(ClienteEntity cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        // 🔹 Eliminar cliente
        public async Task DeleteAsync(string id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        // 🔹 Obtener cliente por correo (útil para login o validaciones)
        public async Task<ClienteEntity?> GetByCorreoAsync(string correo)
        {
            return await _context.Clientes
                                 .FirstOrDefaultAsync(c => c.Correo == correo);
        }

        public async Task<ClienteEntity?> GetByNombreUsuarioAsync(string nombreUsuario)
        {
            return await _context.Clientes
                                 .FirstOrDefaultAsync(c => c.NombreUsuario == nombreUsuario);
        }
    }
}
