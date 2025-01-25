using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    public class BindingsModel : PageModel
    {
        public Product Data { get; set; } = new Product { Name = "Skis", Price = 500 };

        public async Task OnPostAsync([FromForm] bool bind)
        {
            if (bind)
                await TryUpdateModelAsync(Data, "data", p => p.Name, p => p.Price);
        }

        public void OnGet()
        {
        }
    }
}