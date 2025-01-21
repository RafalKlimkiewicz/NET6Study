using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    public class BindingsModel : PageModel
    {
        //[BindProperty(Name = "Data")]
        [FromQuery(Name = "Data")]
        public Product[] Data { get; set; } = Array.Empty<Product>();
        //public SortedSet<string> Data { get; set; } = new SortedSet<string>();

        public void OnGet()
        {
        }
    }
}