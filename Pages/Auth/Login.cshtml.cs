using gestor_de_tareas_personales_practica.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace gestor_de_tareas_personales_practica.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
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

            var user = await _authService.Authenticate(Input.Username, Input.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos.");
                return Page();
            }

            // Crear una cookie de autenticación
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        // Puedes agregar más claims si es necesario
    };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                "CookieAuth",
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Redirigir al usuario a la pantalla principal
            return RedirectToPage("/Index");
        }

        public class LoginInputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}