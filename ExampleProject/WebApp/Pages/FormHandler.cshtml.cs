using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.DB;
using WebApp.Validation;

namespace WebApp.Pages
{
    public class FormHandlerModel : PageModel
    {
        private DataContext _dataContext;

        [BindProperty]
        public Product? Product { get; set; }

        public Category? Category { get; set; } = new();

        public FormHandlerModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task OnGetAsync(long id = 1)
        {
            Product = await _dataContext.Products.FirstAsync(p => p.ProductId == id);
        }

        public IActionResult OnPost()
        {
            //var validationContext = new ValidationContext(Product, serviceProvider: null, items: null);
            //var validationResults = new List<ValidationResult>();

            //bool isValid = Validator.TryValidateObject(Product, validationContext, validationResults, validateAllProperties: true);

            if (!ModelState.IsValid)
            {
                ModelState.PromotePropertyErrors(nameof(Product));
                return Page();
            }

            TempData["name"] = Product?.Name;
            TempData["price"] = Product?.Price.ToString();
            TempData["categoryId"] = Product?.CategoryId.ToString();
            TempData["supplierId"] = Product?.SupplierId.ToString();

            return RedirectToPage("FormResults");
        }
    }
}