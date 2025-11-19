using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using AReyes.DTO;
using AReyes.Services.Interfaces;
using AReyes.Repositories.Interfaces;

namespace AReyes.Services.Implementations
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repo;

        public ProductoService(IProductoRepository repo)
        {
            _repo = repo;
        }

        // GET → Obtener todos los productos
        public async Task<IEnumerable<ProductoEntity>> GetAllAsync()
        {
            var productos = await _repo.GetAllAsync();

            if (productos == null || !productos.Any())
                throw new InvalidOperationException("No hay productos registrados.");

            return productos;
        }

        // GET → Obtener un producto por ID
        public async Task<ProductoEntity?> GetByIdAsync(string productoId)
        {
            if (string.IsNullOrWhiteSpace(productoId))
                throw new ArgumentException("El ID es obligatorio");

            var producto = await _repo.GetByIdAsync(productoId);

            if (producto == null)
                throw new InvalidOperationException("Producto no encontrado");

            return producto;
        }
        // POST → Crear un nuevo producto
        public async Task CreateAsync(ProductoDTO dto)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(dto.NombreProducto))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.");
            }

            // Validar cantidad negativa o no entera
            if (dto.Cantidad < 0 || dto.Cantidad % 1 != 0)
            {
                throw new ArgumentException("La cantidad debe ser un número entero y no negativa.");
            }

            // Validar precio
            if (dto.Precio <= 0)
            {
                throw new ArgumentException("El precio debe ser mayor a cero.");
            }

            // ✅ Validar imagen
            if (string.IsNullOrWhiteSpace(dto.Imagen))
            {
                throw new ArgumentException("La imagen del producto es obligatoria.");
            }

            // Validar formato de imagen (ej. archivo o URL)
            var extensionesValidas = new[] { ".jpg", ".jpeg", ".png" };
            if (!extensionesValidas.Any(ext => dto.Imagen.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("La imagen debe estar en formato JPG o PNG.");
            }

            var producto = new ProductoEntity
            {
                NombreProducto = dto.NombreProducto.Trim(),
                Cantidad = dto.Cantidad,
                Precio = dto.Precio,
                Descripcion = dto.Descripcion?.Trim(),
                Imagen = dto.Imagen.Trim()
            };

            await _repo.CreateAsync(producto);
        }


        // PUT → Actualizar producto
        public async Task UpdateAsync(string id, ProductoDTO dto)
        {
            // Validación básica del ID
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("El ID es obligatorio para actualizar");
            }

            // Buscar si existe el producto
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
            {
                throw new InvalidOperationException("El producto no existe");
            }

            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(dto.NombreProducto))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.");
            }

            if (dto.Cantidad < 0 || dto.Cantidad % 1 != 0)
            {
                throw new ArgumentException("La cantidad debe ser un número entero y no negativa.");
            }

            if (dto.Precio <= 0)
            {
                throw new ArgumentException("El precio debe ser mayor a cero.");
            }

            // Mapear DTO → actualizar entidad existente
            existing.NombreProducto = dto.NombreProducto.Trim();
            existing.Cantidad = dto.Cantidad;
            existing.Precio = dto.Precio;
            existing.Descripcion = dto.Descripcion?.Trim();

            // Guardar cambios
            await _repo.UpdateAsync(existing);
        }


        // DELETE → Eliminar producto por ID
        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es obligatorio para eliminar");

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"No se encontró el producto con Id '{id}'.");

            await _repo.DeleteAsync(id);
        }
    }
}
