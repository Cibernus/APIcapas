using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;
using Areyes.BaseDedatos;
using AReyes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AReyes.Repositories.Implementations
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly AbarrotesReyesContext _context;

        public EmpleadoRepository(AbarrotesReyesContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todos los empleados
        public async Task<IEnumerable<EmpleadoEntity>> GetAllAsync()
        {
            return await _context.Empleados.ToListAsync();
        }

        // 🔹 Obtener empleado por ID
        public async Task<EmpleadoEntity?> GetByIdAsync(string id)
        {
            return await _context.Empleados.FindAsync(id);
        }

        // 🔹 Crear empleado
        public async Task CreateAsync(EmpleadoEntity empleado)
        {
            await _context.Empleados.AddAsync(empleado);
            await _context.SaveChangesAsync();
        }

        // 🔹 Actualizar empleado
        public async Task UpdateAsync(EmpleadoEntity empleado)
        {
            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();
        }

        // 🔹 Eliminar empleado
        public async Task DeleteAsync(string id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<EmpleadoEntity?> GetByCurpAsync(string curp)
        {
            return await _context.Empleados
                                 .FirstOrDefaultAsync(e => e.Curp == curp);
        }

    }
}
