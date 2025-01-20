using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Pages
{
    public class FormHandlerModel : PageModel
    {
        private DataContext _dataContext;

        [BindProperty]
        public Product? Product { get; set; }

        [BindProperty(Name = "Product.Category")]
        public Category? Category { get; set; } = new();

        public FormHandlerModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task OnGetAsync(long id = 1)
        {
            Product = await _dataContext.Products.Include(p => p.Category)
                .Include(p => p.Supplier).FirstAsync(p => p.ProductId == id);
        }

        public IActionResult OnPost()
        {
            TempData["product"] = JsonSerializer.Serialize(Product);
            TempData["category"] = JsonSerializer.Serialize(Category);

            return RedirectToPage("FormResults");
        }
    }
}
