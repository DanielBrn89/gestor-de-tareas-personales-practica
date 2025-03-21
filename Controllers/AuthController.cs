using gestor_de_tareas_personales_practica.Services;
using gestor_de_tareas_personales_practica.Models;
using Microsoft.AspNetCore.Mvc;

namespace gestor_de_tareas_personales_practica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _authService.Authenticate(model.Username, model.Password);

            if (user == null)
                return Unauthorized();

            // Genera un token JWT o maneja la sesión como prefieras
            return Ok(new { user.IdUser, user.Username });
        }
    }
}
