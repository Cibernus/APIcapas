using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using Areyes.BaseDedatos;
using AReyes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AReyes.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AbarrotesReyesContext _context;

        public UsuarioRepository(AbarrotesReyesContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos los usuarios
        public async Task<IEnumerable<UsuarioEntity>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // 🔹 Obtener usuario por ID
        public async Task<UsuarioEntity?> GetByIdAsync(string id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        // 🔹 Crear usuario
        public async Task CreateAsync(UsuarioEntity usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        // 🔹 Actualizar usuario
        public async Task UpdateAsync(UsuarioEntity usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        // 🔹 Eliminar usuario
        public async Task DeleteAsync(string id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UsuarioEntity?> GetByCorreoAsync(string correo)
        {
            return await _context.Usuarios
                                 .FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
        }




    }
}
