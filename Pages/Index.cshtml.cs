using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gestor_de_tareas_personales_practica.Pages
{
    [Authorize] // Solo usuarios autenticados pueden acceder a esta página
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            // Aquí puedes agregar lógica adicional si es necesario
        }
    }
}