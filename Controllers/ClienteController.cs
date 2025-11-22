using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AReyes.DTO;
using AReyes.Services.Interfaces;

namespace AReyes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        // GET: api/cliente
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var clientes = await _service.GetAllAsync();
                return Ok(clientes);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = ex.Message });
            }
        }

        // GET: api/cliente/getClientes/{ClienteId}
        [HttpGet("getClientes/{ClienteId}")]
        public async Task<IActionResult> GetClienteById(string ClienteId)
        {
            try
            {
                var cliente = await _service.GetByIdAsync(ClienteId);
                return Ok(cliente);
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

        // POST: api/cliente
        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteDTO dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return Ok(new { Mensaje = "Cliente creado exitosamente." });
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

        // PUT: api/cliente/putClientes/{ClienteId}
        [HttpPut("putClientes/{ClienteId}")]
        public async Task<IActionResult> ActualizarCliente(string ClienteId, [FromBody] ClienteDTO dto)
        {
            try
            {
                await _service.UpdateAsync(ClienteId, dto);
                return Ok(new { Mensaje = "Cliente actualizado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = $"No se encontró el cliente con Id '{ClienteId}'." });
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

        // DELETE: api/cliente/deleteClientes/{ClienteId}
        [HttpDelete("deleteClientes/{ClienteId}")]
        public async Task<IActionResult> EliminarCliente(string ClienteId)
        {
            try
            {
                await _service.DeleteAsync(ClienteId);
                return Ok(new { Mensaje = "Cliente eliminado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = ex.Message });
            }
        }
    }
}
