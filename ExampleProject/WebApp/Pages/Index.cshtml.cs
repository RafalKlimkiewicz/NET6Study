using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _dataContext;

        public Product? Product;

        public IndexModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> OnGetAsync(long id = 1)
        {
            Product = await _dataContext.Products.FindAsync(id);
            
            if (Product == null)
                return RedirectToPage("NotFound");

            return Page();
        }
    }
}