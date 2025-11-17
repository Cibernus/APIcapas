using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AReyes.DTO;
using AReyes.Services.Interfaces;

namespace AReyes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _service;

        public ProveedorController(IProveedorService service)
        {
            _service = service;
        }

        // GET: api/proveedor
        [HttpGet]
        public async Task<IActionResult> GetProveedores()
        {
            try
            {
                var proveedores = await _service.GetAllAsync();
                return Ok(proveedores);
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

        // GET: api/proveedor/{ProveedorId}
        [HttpGet("getProveedor/{ProveedorId}")]
        public async Task<IActionResult> GetProveedorById(string ProveedorId)
        {
            try
            {
                var proveedor = await _service.GetByIdAsync(ProveedorId);
                return Ok(proveedor);
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

        // POST: api/proveedor
        [HttpPost]
        public async Task<IActionResult> CrearProveedor([FromBody] ProveedorDTOcs dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return Ok(new { Mensaje = "Proveedor creado exitosamente." });
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

        // PUT: api/proveedor/{ProveedorId}
        [HttpPut("putProveedores/{ProveedorId}")]
        public async Task<IActionResult> ActualizarProveedor(string ProveedorId, [FromBody] ProveedorDTOcs dto)
        {
            try
            {
                await _service.UpdateAsync(ProveedorId, dto);
                return Ok(new { Mensaje = "Proveedor actualizado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = $"No se encontró el proveedor con Id '{ProveedorId}'." });
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

        // DELETE: api/proveedor/{ProveedorId}
        [HttpDelete("deleteProveedores/{ProveedorId}")]
        public async Task<IActionResult> EliminarProveedor(string ProveedorId)
        {
            try
            {
                await _service.DeleteAsync(ProveedorId);
                return Ok(new { Mensaje = "Proveedor eliminado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = ex.Message });
            }
        }
    }
}
