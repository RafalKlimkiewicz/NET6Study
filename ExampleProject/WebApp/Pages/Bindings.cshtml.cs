using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class BindingsModel : PageModel
    {
        [BindProperty(Name = "Data")]
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
        //public SortedSet<string> Data { get; set; } = new SortedSet<string>();

        public void OnGet()
        {
        }
    }
}