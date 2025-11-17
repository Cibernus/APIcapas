using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using AReyes.DTO;
using AReyes.Services.Interfaces;
using AReyes.Repositories.Interfaces;
using AReyes.Migrations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AReyes.Services.Implementations
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _repo;

        public EmpleadoService(IEmpleadoRepository repo)
        {
            _repo = repo;
        }
        // GET → Obtener todos los empleados
        public async Task<IEnumerable<EmpleadoEntity>> GetAllAsync()
        {
            try
            {
                var empleados = await _repo.GetAllAsync();

                if (empleados == null || !empleados.Any())
                {
                    // Lanzamos excepción para que el Controller decida si devuelve 404
                    throw new InvalidOperationException("No hay empleados registrados.");
                }

                return empleados;
            }
            catch (Exception ex)
            {
                // Aquí capturamos cualquier error inesperado y lo propagamos
                throw new Exception($"Error al obtener los empleados: {ex.Message}", ex);
            }
        }
        // GET → Obtener un empleado por ID
        public async Task<EmpleadoEntity?> GetByIdAsync(string EmpleadoId)
        {
            if (string.IsNullOrWhiteSpace(EmpleadoId))
                throw new ArgumentException("El ID es obligatorio");

            var empleado = await _repo.GetByIdAsync(EmpleadoId);

            if (empleado == null)
                throw new InvalidOperationException("Empleado no encontrado");

            return empleado;
        }
        //POST
        public async Task CreateAsync(EmpleadoDTO dto)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(dto.NombreEmpleado))
                throw new ArgumentException("El nombre es obligatorio.");
            if (System.Text.RegularExpressions.Regex.IsMatch(dto.NombreEmpleado.Trim(), @"^\d+$"))
                throw new ArgumentException("El nombre no puede ser numérico.");

            if (string.IsNullOrWhiteSpace(dto.ApellidoPaterno))
                throw new ArgumentException("El apellido paterno es obligatorio.");
            if (System.Text.RegularExpressions.Regex.IsMatch(dto.ApellidoPaterno.Trim(), @"^\d+$"))
                throw new ArgumentException("El apellido paterno no puede ser numérico.");

            if (!string.IsNullOrWhiteSpace(dto.ApellidoMaterno) &&
                System.Text.RegularExpressions.Regex.IsMatch(dto.ApellidoMaterno.Trim(), @"^\d+$"))
                throw new ArgumentException("El apellido materno no puede ser numérico.");

            if (string.IsNullOrWhiteSpace(dto.Curp))
                throw new ArgumentException("La CURP es obligatoria.");
            if (dto.Curp.Length != 18)
                throw new ArgumentException("La CURP debe tener 18 caracteres.");
            if (!System.Text.RegularExpressions.Regex.IsMatch(dto.Curp, "^[A-Z0-9]{18}$"))
                throw new ArgumentException("La CURP debe contener solo letras mayúsculas y números.");

            // Normalización
            dto.Curp = dto.Curp.ToUpperInvariant().Trim();

            // 👇 Validar que la CURP no exista antes de insertar
            var empleadoConCurp = await _repo.GetByCurpAsync(dto.Curp);
            if (empleadoConCurp != null)
                throw new ArgumentException("La CURP ya está registrada, no se puede insertar.");

            // Mapear DTO → Entidad (sin ID)
            var empleado = new EmpleadoEntity
            {
                NombreEmpleado = dto.NombreEmpleado.Trim(),
                ApellidoPaterno = dto.ApellidoPaterno.Trim(),
                ApellidoMaterno = dto.ApellidoMaterno?.Trim(),
                Curp = dto.Curp,
                Telefono = dto.Telefono.Trim()
            };

            // Guardar en la base
            await _repo.CreateAsync(empleado);
        }


        public async Task UpdateAsync(string Id, EmpleadoDTO dto)
        {
            // 🟤 Validar que el ID venga informado
            if (string.IsNullOrWhiteSpace(Id))
            {
                throw new ArgumentException("El ID es obligatorio para actualizar");
            }

            // 🟤 Buscar el empleado existente por ID
            var existing = await _repo.GetByIdAsync(Id);
            if (existing == null)
            {
                throw new InvalidOperationException("El empleado no existe");
            }

            // 🟤 Validaciones de negocio sobre los campos
            if (string.IsNullOrWhiteSpace(dto.NombreEmpleado))
            {
                throw new ArgumentException("El nombre es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(dto.ApellidoPaterno))
            {
                throw new ArgumentException("El apellido paterno es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(dto.Curp) || dto.Curp.Length != 18)
            {
                throw new ArgumentException("La CURP debe tener 18 caracteres");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(dto.Curp, "^[A-Z0-9]{18}$"))
            {
                throw new ArgumentException("La CURP debe contener solo letras mayúsculas y números");
            }

            if (string.IsNullOrWhiteSpace(dto.Telefono) || dto.Telefono.Length < 10 || dto.Telefono.Length > 15)
            {
                throw new ArgumentException("El teléfono debe tener entre 10 y 15 dígitos");
            }

            // Normalizar CURP
            var curp = dto.Curp.Trim().ToUpperInvariant();

            // 🟤 Validar que la CURP no esté duplicada en otro empleado
            var empleadoConCurp = await _repo.GetByCurpAsync(curp);
            if (empleadoConCurp != null && empleadoConCurp.EmpleadoId != Id)
            {
                throw new ArgumentException("La CURP ya está registrada en otro empleado.");
            }

            //  Mapear DTO → actualizar entidad existente
            existing.NombreEmpleado = dto.NombreEmpleado.Trim();
            existing.ApellidoPaterno = dto.ApellidoPaterno.Trim();
            existing.ApellidoMaterno = dto.ApellidoMaterno?.Trim();
            existing.Curp = curp;
            existing.Telefono = dto.Telefono.Trim();

            //  Guardar cambios en la base
            await _repo.UpdateAsync(existing);
        }

        // DELETE → Eliminar un empleado por ID
        public async Task DeleteAsync(string id)
        {
            // Validación básica
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es obligatorio para eliminar");

            // Buscar si existe el empleado
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"No se encontró el empleado con Id '{id}'.");

            // Eliminar
            await _repo.DeleteAsync(id);
        }

    }
}
