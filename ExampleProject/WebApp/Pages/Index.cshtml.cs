using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private DataContext _dataContext;

        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

        public IndexModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void OnGetAsync(long id = 1)
        {
            Products = _dataContext.Products.Include(p => p.Category).Include(p => p.Supplier);
        }
    }
}
