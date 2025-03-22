using gestor_de_tareas_personales_practica.Services;
using gestor_de_tareas_personales_practica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using static gestor_de_tareas_personales_practica.Services.AuthService;

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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null || !PasswordHasher.VerifyPassword(model.Password, user.PasswordHash))
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");
            }

            // Aquí puedes generar un token JWT o manejar la sesión
            return Ok(new { user.Id_User, user.Username });
        }
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el usuario ya existe
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (existingUser != null)
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            // Encriptar la contraseña
            var passwordHash = PasswordHasher.HashPassword(model.Password);

            // Crear el nuevo usuario
            var user = new User
            {
                Username = model.Username,
                PasswordHash = passwordHash
            };

            // Guardar el usuario en la base de datos
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { user.Id_User, user.Username });
        }
        public async Task<User> Authenticate(string username, string password)
        {
            Console.WriteLine($"Buscando usuario: {username}");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado");
                return null;
            }

            Console.WriteLine($"Usuario encontrado: {user.Username}");
            Console.WriteLine($"Verificando contraseña...");

            bool isPasswordValid = VerifyPasswordHash(password, user.PasswordHash);

            if (!isPasswordValid)
            {
                Console.WriteLine("Contraseña incorrecta");
                return null;
            }

            Console.WriteLine("Usuario autenticado correctamente");
            return user;
        }

        private bool VerifyPasswordHash(string password, string passwordHash)
        {
            throw new NotImplementedException();
        }
    }

}
