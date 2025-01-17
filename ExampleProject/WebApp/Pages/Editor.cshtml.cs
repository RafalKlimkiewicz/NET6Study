using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Pages
{
    public class EditorModel : PageModel
    {
        private readonly DataContext _context;

        public Product? Product { get; set; } = new();

        public EditorModel(DataContext context)
        {
            _context = context;
        }

        public async void OnGetAsync(long id)
        {
            Product = await _context.Products.FindAsync(id) ?? new();
        }

        public async Task<IActionResult> OnPostAsync(long id, decimal price)
        {
            var p = await _context.Products.FindAsync(id);
            
            if (p != null)
                p.Price = price;

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
