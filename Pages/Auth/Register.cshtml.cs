using gestor_de_tareas_personales_practica.Models;
using gestor_de_tareas_personales_practica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using System.Threading.Tasks;
using static gestor_de_tareas_personales_practica.Services.AuthService;

namespace gestor_de_tareas_personales_practica.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var user = new User
                {
                    Username = Input.Username,
                    PasswordHash = PasswordHasher.HashPassword(Input.Password)
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToPage("/Auth/Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar el usuario: " + ex.Message);
                return Page();
            }
        }
    }


    public class RegisterInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}