using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.DB;

namespace WebApp.Pages.Suppliers
{
    public class ListModel : PageModel
    {
        private readonly DataContext _context;

        public IEnumerable<string> Suppliers { get; set; } = Enumerable.Empty<string>();

        public ListModel(DataContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Suppliers = _context.Suppliers.Select(x => x.Name);  
        }
    }
}