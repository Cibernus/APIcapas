using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using AReyes.DTO;
using AReyes.Services.Interfaces;
using AReyes.Repositories.Interfaces;
using BCrypt.Net;

namespace AReyes.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        // GET → Obtener todos los usuarios
        public async Task<IEnumerable<UsuarioEntity>> GetAllAsync()
        {
            var usuarios = await _repo.GetAllAsync();

            if (usuarios == null || !usuarios.Any())
                throw new InvalidOperationException("No hay usuarios registrados.");

            return usuarios;
        }

        // GET → Obtener un usuario por ID
        public async Task<UsuarioEntity?> GetByIdAsync(string usuarioId)
        {
            if (string.IsNullOrWhiteSpace(usuarioId))
                throw new ArgumentException("El ID es obligatorio");

            var usuario = await _repo.GetByIdAsync(usuarioId);

            if (usuario == null)
                throw new InvalidOperationException("Usuario no encontrado");

            return usuario;
        }

        //POST PARA CREAR USUARIOS
        public async Task CreateAsync(UsuarioDTO dto)
        {
            //  Validaciones básicas
            if (string.IsNullOrWhiteSpace(dto.NombreUsuario))
            {
                throw new ArgumentException("El nombre del usuario es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(dto.ApellidoPaterno))
            {
                throw new ArgumentException("El apellido paterno es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(dto.Telefono) || dto.Telefono.Length < 10 || dto.Telefono.Length > 15)
            {
                throw new ArgumentException("El teléfono debe tener entre 10 y 15 dígitos.");
            }

            if (string.IsNullOrWhiteSpace(dto.CorreoElectronico))
            {
                throw new ArgumentException("El correo electrónico es obligatorio.");
            }

            if (dto.CorreoElectronico.Length > 150)
            {
                throw new ArgumentException("El correo electrónico no puede exceder los 150 caracteres.");
            }

            // 🔹 Validar que no exista un usuario con el mismo correo
            var usuarioConCorreo = await _repo.GetByCorreoAsync(dto.CorreoElectronico.Trim().ToLowerInvariant());
            if (usuarioConCorreo != null)
            {
                throw new ArgumentException("El correo electrónico ya está registrado, no se puede insertar.");
            }

            if (string.IsNullOrWhiteSpace(dto.Contrasena)|| dto.Contrasena.Length < 8)
            {
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(dto.Rol) ||
                (dto.Rol != "Administrador" && dto.Rol != "Empleado"))
            {
                throw new ArgumentException("El rol debe ser 'Administrador' o 'Empleado'.");
            }

            // Normalizar correo
            dto.CorreoElectronico = dto.CorreoElectronico.Trim().ToLowerInvariant();

            //  Mapear DTO → Entidad
            var usuario = new UsuarioEntity
            {
                NombreUsuario = dto.NombreUsuario.Trim(),
                ApellidoPaterno = dto.ApellidoPaterno.Trim(),
                ApellidoMaterno = dto.ApellidoMaterno?.Trim(),
                Telefono = dto.Telefono.Trim(),
                CorreoElectronico = dto.CorreoElectronico,
                Contrasena = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena.Trim()),
                Rol = dto.Rol,

            };

            await _repo.CreateAsync(usuario);
        }

        // PUT → Actualizar usuario
        public async Task UpdateAsync(string id, UsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es obligatorio para actualizar");

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException("El usuario no existe");

            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(dto.NombreUsuario))
                throw new ArgumentException("El nombre del usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.ApellidoPaterno))
                throw new ArgumentException("El apellido paterno es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Telefono) || dto.Telefono.Length < 10 || dto.Telefono.Length > 15)
                throw new ArgumentException("El teléfono debe tener entre 10 y 15 dígitos.");

            if (string.IsNullOrWhiteSpace(dto.CorreoElectronico))
                throw new ArgumentException("El correo electrónico es obligatorio.");

            if (dto.CorreoElectronico.Length > 150)
                throw new ArgumentException("El correo electrónico no puede exceder los 150 caracteres.");

            // Validar que el correo no esté duplicado en otro usuario
            var usuarioConCorreo = await _repo.GetByCorreoAsync(dto.CorreoElectronico.Trim().ToLowerInvariant());
            if (usuarioConCorreo != null && usuarioConCorreo.UsuarioId != id)
                throw new ArgumentException("El correo electrónico ya está registrado en otro usuario.");

            if (string.IsNullOrWhiteSpace(dto.Rol) ||
                (dto.Rol != "Administrador" && dto.Rol != "Empleado"))
                throw new ArgumentException("El rol debe ser 'Administrador' o 'Empleado'.");

            // Normalizar correo
            var correo = dto.CorreoElectronico.Trim().ToLowerInvariant();

            // Mapear DTO → actualizar entidad existente
            existing.NombreUsuario = dto.NombreUsuario.Trim();
            existing.ApellidoPaterno = dto.ApellidoPaterno.Trim();
            existing.ApellidoMaterno = dto.ApellidoMaterno?.Trim();
            existing.Telefono = dto.Telefono.Trim();
            existing.CorreoElectronico = correo;
            existing.Rol = dto.Rol;

            // 🔹 Solo si se envía una nueva contraseña
            if (!string.IsNullOrWhiteSpace(dto.Contrasena))
            {
                if (dto.Contrasena.Length < 8)
                    throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");

                existing.Contrasena = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena.Trim());
            }

            await _repo.UpdateAsync(existing);
        }



        // DELETE → Eliminar usuario por ID
        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es obligatorio para eliminar");

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"No se encontró el usuario con Id '{id}'.");

            await _repo.DeleteAsync(id);
        }
    }
}
