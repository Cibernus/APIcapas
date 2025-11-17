using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AReyes.Services;
using AReyes.DTO;
using AReyes.Services.Interfaces;

namespace AReyes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _service;

        public EmpleadoController(IEmpleadoService service)
        {
            _service = service;
        }

        // GET: api/empleado
        [HttpGet]
        public async Task<IActionResult> GetEmpleados()
        {
            try
            {
                var empleados = await _service.GetAllAsync();
                return Ok(empleados);
            }
            catch (InvalidOperationException ex)
            {
                // Caso esperado: no hay registros
                return NotFound(new { Mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                // Caso inesperado: error interno
                return StatusCode(500, new { Mensaje = ex.Message });
            }
        }

        [HttpGet("getEmpleados/{EmpleadoId}")]
        public async Task<IActionResult> GetEmpleadoById(string EmpleadoId)
        {
            try
            {
                var empleado = await _service.GetByIdAsync(EmpleadoId);
                return Ok(empleado);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Mensaje = ex.Message });
            }
        }
        // POST: api/empleado
        [HttpPost]
        public async Task<IActionResult> CrearEmpleado([FromBody] EmpleadoDTO dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return Ok(new { Mensaje = "Empleado creado exitosamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Mensaje = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new
                {
                    success = false,
                    mensaje = "Error al guardar en la base de datos.",
                    detalles = ex.InnerException?.Message ?? ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
        // PUT: api/empleado/{id}
        [HttpPut("putEmpleados/{EmpleadoId}")]
        public async Task<IActionResult> ActualizarEmpleado(string EmpleadoId, [FromBody] EmpleadoDTO dto)
        {
            try
            {
                await _service.UpdateAsync(EmpleadoId, dto); // 👈 id por parámetro + DTO en body
                return Ok(new { Mensaje = "Empleado actualizado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = $"No se encontró el empleado con Id '{EmpleadoId}'." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Error = "Validación de datos",
                    Detalle = ex.Message
                });
            }
        }

        // DELETE: api/empleado/{id}
        [HttpDelete("deleteEmpleados/{EmpleadoId}")]
        public async Task<IActionResult> EliminarEmpleado(string EmpleadoId)
        {
            try
            {
                await _service.DeleteAsync(EmpleadoId);
                return Ok(new { Mensaje = "Empleado eliminado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = ex.Message });
            }
        }
    }
}

