using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gestor_de_tareas_personales_practica.Pages
{
    [Authorize] // Solo usuarios autenticados pueden acceder a esta p�gina
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            // Aqu� puedes agregar l�gica adicional si es necesario
        }
    }
}