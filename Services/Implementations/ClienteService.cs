using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using AReyes.DTO;
using AReyes.Services.Interfaces;
using AReyes.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AReyes.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        // GET → Obtener todos los clientes
        public async Task<IEnumerable<ClienteEntity>> GetAllAsync()
        {
            try
            {
                var clientes = await _repo.GetAllAsync();

                if (clientes == null || !clientes.Any())
                {
                    throw new InvalidOperationException("No hay clientes registrados.");
                }

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los clientes: {ex.Message}", ex);
            }
        }

        // GET → Obtener un cliente por ID
        public async Task<ClienteEntity?> GetByIdAsync(string clienteId)
        {
            if (string.IsNullOrWhiteSpace(clienteId))
                throw new ArgumentException("El ID es obligatorio");

            var cliente = await _repo.GetByIdAsync(clienteId);

            if (cliente == null)
                throw new InvalidOperationException("Cliente no encontrado");

            return cliente;
        }
        // POST → Crear un nuevo cliente
        public async Task CreateAsync(ClienteDTO dto)
        {
            // 🔹 Validaciones básicas
            if (string.IsNullOrWhiteSpace(dto.NombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");
            if (System.Text.RegularExpressions.Regex.IsMatch(dto.NombreUsuario.Trim(), @"^\d+$"))
                throw new ArgumentException("El nombre de usuario no puede ser numérico.");

            if (string.IsNullOrWhiteSpace(dto.Correo))
                throw new ArgumentException("El correo es obligatorio.");
            if (!System.Text.RegularExpressions.Regex.IsMatch(dto.Correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("El correo no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(dto.Contrasena))
                throw new ArgumentException("La contraseña es obligatoria.");
            if (dto.Contrasena.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.");

            // 🔹 Validar que el correo no exista antes de insertar
            var clienteConCorreo = await _repo.GetByCorreoAsync(dto.Correo.Trim());
            if (clienteConCorreo != null)
                throw new ArgumentException("El correo ya está registrado, no se puede insertar.");

            // 🔹 Validar que el nombre de usuario no exista antes de insertar
            var clienteConNombreUsuario = await _repo.GetByNombreUsuarioAsync(dto.NombreUsuario.Trim());
            if (clienteConNombreUsuario != null)
                throw new ArgumentException("El nombre de usuario ya está registrado, no se puede insertar.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena.Trim());

            // 🔹 Mapear DTO → Entidad
            var cliente = new ClienteEntity
            {
                NombreUsuario = dto.NombreUsuario.Trim(),
                Correo = dto.Correo.Trim(),
                Contrasena = hashedPassword // ⚠️ aquí deberías guardar un hash, no texto plano
            };

            await _repo.CreateAsync(cliente);
        }
        // PUT → Actualizar un cliente existente
        public async Task UpdateAsync(string id, ClienteDTO dto)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es obligatorio para actualizar");


            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException("El cliente no existe");

            if (string.IsNullOrWhiteSpace(dto.NombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Correo))
                throw new ArgumentException("El correo es obligatorio.");
            if (!System.Text.RegularExpressions.Regex.IsMatch(dto.Correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("El correo no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(dto.Contrasena))
                throw new ArgumentException("La contraseña es obligatoria.");
            if (dto.Contrasena.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.");

            // 🔹 Validar que el correo no esté duplicado en otro cliente
            var clienteConCorreo = await _repo.GetByCorreoAsync(dto.Correo.Trim());
            if (clienteConCorreo != null && clienteConCorreo.ClienteId != id)
                throw new ArgumentException("El correo ya está registrado en otro cliente.");

            // 🔹 Validar que el nombre de usuario no esté duplicado en otro cliente
            var clienteConNombreUsuario = await _repo.GetByNombreUsuarioAsync(dto.NombreUsuario.Trim());
            if (clienteConNombreUsuario != null && clienteConNombreUsuario.ClienteId != id)
                throw new ArgumentException("El nombre de usuario ya está registrado en otro cliente.");

            // Mapear DTO → actualizar entidad existente
            existing.NombreUsuario = dto.NombreUsuario.Trim();
            existing.Correo = dto.Correo.Trim();
            existing.Contrasena = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena.Trim());// ⚠️ aquí deberías guardar hash

            await _repo.UpdateAsync(existing);
        }

        // DELETE → Eliminar un cliente por ID
        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es obligatorio para eliminar");

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"No se encontró el cliente con Id '{id}'.");

            await _repo.DeleteAsync(id);
        }
    }
}

