using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using AReyes.DTO;
using AReyes.Migrations;

namespace AReyes.Services.Interfaces
{
    public interface IEmpleadoService
    {
        // GET → Obtener todos los empleados
        Task<IEnumerable<EmpleadoEntity>> GetAllAsync();

        // GET → Obtener un empleado por ID
        Task<EmpleadoEntity?> GetByIdAsync(string id);

        // POST → Crear un nuevo empleado
        Task CreateAsync(EmpleadoDTO empleado);

        // PUT → Actualizar un empleado existente
        Task UpdateAsync(String id ,EmpleadoDTO empleado);

        // DELETE → Eliminar un empleado por ID
        Task DeleteAsync(string id);

    }
}
