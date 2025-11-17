using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using AReyes.DTO;
using AReyes.Services.Interfaces;
using AReyes.Repositories.Interfaces;

namespace AReyes.Services.Implementations
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _repo;

        public ProveedorService(IProveedorRepository repo)
        {
            _repo = repo;
        }

        // GET → Obtener todos los proveedores
        public async Task<IEnumerable<ProveedorEntity>> GetAllAsync()
        {
            var proveedores = await _repo.GetAllAsync();

            if (proveedores == null || !proveedores.Any())
                throw new InvalidOperationException("No hay proveedores registrados.");

            return proveedores;
        }

        // GET → Obtener un proveedor por ID
        public async Task<ProveedorEntity?> GetByIdAsync(string proveedorId)
        {
            if (string.IsNullOrWhiteSpace(proveedorId))
                throw new ArgumentException("El ID es obligatorio");

            var proveedor = await _repo.GetByIdAsync(proveedorId);

            if (proveedor == null)
                throw new InvalidOperationException("Proveedor no encontrado");

            return proveedor;
        }

        // POST → Crear un nuevo proveedor
        public async Task CreateAsync(ProveedorDTOcs dto)
        {
            // 🟤 Validaciones básicas
            if (string.IsNullOrWhiteSpace(dto.NombreProveedor))
            {
                throw new ArgumentException("El nombre del proveedor es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(dto.ApellidoPaterno))
            {
                throw new ArgumentException("El apellido paterno es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(dto.Rfc))
            {
                throw new ArgumentException("El RFC es obligatorio.");
            }

            if (dto.Rfc.Length != 13)
            {
                throw new ArgumentException("El RFC debe tener 13 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(dto.Telefono) || dto.Telefono.Length < 10 || dto.Telefono.Length > 15)
            {
                throw new ArgumentException("El teléfono debe tener entre 10 y 15 dígitos.");
            }

            // 🟤 Normalizar RFC
            dto.Rfc = dto.Rfc.Trim().ToUpperInvariant();

            // 🟤 Validar que el RFC no esté duplicado
            var proveedorConRfc = await _repo.GetByRfcAsync(dto.Rfc);
            if (proveedorConRfc != null)
            {
                throw new ArgumentException("El RFC ya está registrado, no se puede insertar.");
            }

            // 🟤 Mapear DTO → Entidad
            var proveedor = new ProveedorEntity
            {
                NombreProveedor = dto.NombreProveedor.Trim(),
                ApellidoPaterno = dto.ApellidoPaterno.Trim(),
                ApellidoMaterno = dto.ApellidoMaterno?.Trim(),
                Telefono = dto.Telefono.Trim(),
                Rfc = dto.Rfc
            };

            // 🟤 Guardar en la base
            await _repo.CreateAsync(proveedor);
        }


        // PUT → Actualizar proveedor
        public async Task UpdateAsync(string id, ProveedorDTOcs dto)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("El ID es obligatorio para actualizar");
            }

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
            {
                throw new InvalidOperationException("El proveedor no existe");
            }

            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(dto.NombreProveedor))
            {
                throw new ArgumentException("El nombre del proveedor es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(dto.ApellidoPaterno))
            {
                throw new ArgumentException("El apellido paterno es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(dto.Rfc) || dto.Rfc.Length != 13)
            {
                throw new ArgumentException("El RFC debe tener 13 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(dto.Telefono) || dto.Telefono.Length < 10 || dto.Telefono.Length > 15)
            {
                throw new ArgumentException("El teléfono debe tener entre 10 y 15 dígitos.");
            }

            // 🟤 Normalizar RFC
            var rfc = dto.Rfc.Trim().ToUpperInvariant();

            // 🟤 Validar que el RFC no esté duplicado en otro proveedor
            var proveedorConRfc = await _repo.GetByRfcAsync(rfc);
            if (proveedorConRfc != null && proveedorConRfc.ProveedorId != id)
            {
                throw new ArgumentException("El RFC ya está registrado en otro proveedor.");
            }

            // Mapear DTO → actualizar entidad existente
            existing.NombreProveedor = dto.NombreProveedor.Trim();
            existing.ApellidoPaterno = dto.ApellidoPaterno.Trim();
            existing.ApellidoMaterno = dto.ApellidoMaterno.Trim();
            existing.Telefono = dto.Telefono.Trim();
            existing.Rfc = dto.Rfc.Trim().ToUpperInvariant();

            await _repo.UpdateAsync(existing);
        }

        // DELETE → Eliminar proveedor por ID
        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es obligatorio para eliminar");

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"No se encontró el proveedor con Id '{id}'.");

            await _repo.DeleteAsync(id);
        }
    }
}
