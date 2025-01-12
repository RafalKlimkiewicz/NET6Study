using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Pages
{
    public class HandlerSelectorModel : PageModel
    {
        private readonly DataContext _dataContext;
        public Product? Product { get; set; }

        public HandlerSelectorModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task OnGetAsync(long id = 1)
        {
            Product = await _dataContext.Products.FindAsync(id);
        }

        public async Task OnGetRelatedAsync(long id = 1)
        {
            Product = await _dataContext.Products
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (Product != null && Product.Supplier != null)
                Product.Supplier.Products = null;

            if (Product != null && Product.Category != null)
                Product.Category.Products = null;
        }
    }
}