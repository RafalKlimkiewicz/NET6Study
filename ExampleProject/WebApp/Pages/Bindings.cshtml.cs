using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class BindingsModel : PageModel
    {
        [BindProperty]
        public string[] Data { get; set; } = Array.Empty<string>();

        public void OnGet()
        {
        }
    }
}