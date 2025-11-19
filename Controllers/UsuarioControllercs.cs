using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AReyes.DTO;
using AReyes.Services.Interfaces;

namespace AReyes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var usuarios = await _service.GetAllAsync();
                return Ok(usuarios);
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

        // GET: api/usuario/{UsuarioId}
        [HttpGet("getUsuario/{UsuarioId}")]
        public async Task<IActionResult> GetUsuarioById(string UsuarioId)
        {
            try
            {
                var usuario = await _service.GetByIdAsync(UsuarioId);
                return Ok(usuario);
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

        // POST: api/usuario
        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioDTO dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return Ok(new { Mensaje = "Usuario creado exitosamente." });
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

        // PUT: api/usuario/{UsuarioId}
        [HttpPut("putUsuarios/{UsuarioId}")]
        public async Task<IActionResult> ActualizarUsuario(string UsuarioId, [FromBody] UsuarioDTO dto)
        {
            try
            {
                await _service.UpdateAsync(UsuarioId, dto);
                return Ok(new { Mensaje = "Usuario actualizado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = $"No se encontró el usuario con Id '{UsuarioId}'." });
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

        // DELETE: api/usuario/{UsuarioId}
        [HttpDelete("deleteUsuarios/{UsuarioId}")]
        public async Task<IActionResult> EliminarUsuario(string UsuarioId)
        {
            try
            {
                await _service.DeleteAsync(UsuarioId);
                return Ok(new { Mensaje = "Usuario eliminado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Mensaje = ex.Message });
            }
        }
    }
}
