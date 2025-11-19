using AReyes.Models;

namespace AReyes.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        // GET → Obtener todos los usuarios
        Task<IEnumerable<UsuarioEntity>> GetAllAsync();

        // GET → Obtener un usuario por ID
        Task<UsuarioEntity?> GetByIdAsync(string id);

        // POST → Crear un nuevo usuario
        Task CreateAsync(UsuarioEntity usuario);

        // PUT → Actualizar un usuario existente
        Task UpdateAsync(UsuarioEntity usuario);

        // DELETE → Eliminar un usuario por ID
        Task DeleteAsync(string id);

        Task<UsuarioEntity?> GetByCorreoAsync(string Correo);
    }
}

