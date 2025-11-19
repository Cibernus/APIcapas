using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using AReyes.DTO;

namespace AReyes.Services.Interfaces
{
    public interface IUsuarioService
    {
        // GET → Obtener todos los usuarios
        Task<IEnumerable<UsuarioEntity>> GetAllAsync();

        // GET → Obtener un usuario por ID
        Task<UsuarioEntity?> GetByIdAsync(string id);

        // POST → Crear un nuevo usuario
        Task CreateAsync(UsuarioDTO usuario);

        // PUT → Actualizar un usuario existente
        Task UpdateAsync(string id, UsuarioDTO usuario);

        // DELETE → Eliminar un usuario por ID
        Task DeleteAsync(string id);
    }
}
