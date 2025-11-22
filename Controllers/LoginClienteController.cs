using Microsoft.AspNetCore.Mvc;
using AReyes.DTO;
using AReyes.Services.Interfaces;

namespace AReyes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginClienteController : ControllerBase
    {
        private readonly ILoginClienteService _loginClienteService;

        public LoginClienteController(ILoginClienteService loginClienteService)
        {
            _loginClienteService = loginClienteService;
        }

        [HttpPost("loginCliente")]
        public async Task<IActionResult> Login([FromBody] LoginClienteDTO dto)
        {
            try
            {
                var cliente = await _loginClienteService.LoginAsync(dto);
                return Ok(new { Mensaje = "Login exitoso", Cliente = cliente });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo genérico de errores
                return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message });
            }
        }
    }
}
