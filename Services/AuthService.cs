using gestor_de_tareas_personales_practica.Models;
using Microsoft.EntityFrameworkCore;

namespace gestor_de_tareas_personales_practica.Services
{
    public interface IAuthService
    {
        Task<User> Authenticate(string username, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                return null; // Usuario no encontrado o contraseña incorrecta
            }

            return user; // Usuario autenticado correctamente
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // Implementa la lógica para verificar el hash de la contraseña
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
}

        public static class PasswordHasher
        {
            public static string HashPassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

            public static bool VerifyPassword(string password, string hashedPassword)
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
        }
    }
