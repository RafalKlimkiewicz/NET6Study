using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Pages
{
    public class NotFoundModel : PageModel
    {
        private readonly DataContext _context;

        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

        public NotFoundModel(DataContext context)
        {
            _context = context;
        }

        public void OnGetAsync(long id = 1)
        {
            Products = _context.Products;
        }
    }
}