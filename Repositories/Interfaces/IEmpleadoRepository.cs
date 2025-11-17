using System.Collections.Generic;
using System.Threading.Tasks;
using AReyes.Models;

namespace AReyes.Repositories.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<EmpleadoEntity>> GetAllAsync();
        Task<EmpleadoEntity?> GetByIdAsync(string id);
        Task CreateAsync(EmpleadoEntity empleado);
        Task UpdateAsync(EmpleadoEntity empleado);
        Task DeleteAsync(string id);

        Task<EmpleadoEntity?> GetByCurpAsync(string curp);

    }
}
