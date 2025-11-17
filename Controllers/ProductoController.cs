using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AReyes.DTO;
using AReyes.Services.Interfaces;

namespace AReyes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        // GET: api/producto
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            try
            {
                var productos = await _service.GetAllAsync();
                return Ok(productos);
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

        // GET: api/producto/{ProductoId}
        [HttpGet("getProductos/{ProductoId}")]
        public async Task<IActionResult> GetProductoById(string ProductoId)
        {
            try
            {
                var producto = await _service.GetByIdAsync(ProductoId);
                return Ok(producto);
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

        // POST: api/producto
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoDTO dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return Ok(new { Mensaje = "Producto creado exitosamente." });
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

        // PUT: api/producto/{ProductoId}
        [HttpPut("putProductos/{ProductoId}")]
        public async Task<IActionResult> ActualizarProducto(string ProductoId, [FromBody] ProductoDTO dto)
        {
            try
            {
                await _service.UpdateAsync(ProductoId, dto);
                return Ok(new { Mensaje = "Producto actualizado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = $"No se encontró el producto con Id '{ProductoId}'." });
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

        // DELETE: api/producto/{ProductoId}
        [HttpDelete("deleteProductos/{ProductoId}")]
        public async Task<IActionResult> EliminarProducto(string ProductoId)
        {
            try
            {
                await _service.DeleteAsync(ProductoId);
                return Ok(new { Mensaje = "Producto eliminado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = ex.Message });
            }
        }
    }
}
