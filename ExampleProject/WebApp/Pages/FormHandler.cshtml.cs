using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Pages
{
    [IgnoreAntiforgeryToken]
    public class FormHandlerModel : PageModel
    {
        private DataContext _dataContext;

        public Product? Product { get; set; }

        public FormHandlerModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task OnGetAsync(long id = 1)
        {
            Product = await _dataContext.Products.FindAsync(id);
        }

        public IActionResult OnPost()
        {
            foreach (var key in Request.Form.Keys.Where(k => !k.StartsWith("_")))
            {
                TempData[key] = string.Join(", ", Request.Form[key]);
            }

            return RedirectToPage("FormResults");
        }
    }
}
