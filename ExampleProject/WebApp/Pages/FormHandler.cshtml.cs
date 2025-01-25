using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            var name = nameof(Product.Price);

            if (ModelState.GetValidationState("Product.Price") == ModelValidationState.Valid && Product.Price < 1)
            {
                ModelState.AddModelError("Product.Price", "Enter a positive price");
            }

            if (ModelState.GetValidationState("Product.Name") == ModelValidationState.Valid 
                && ModelState.GetValidationState("Product.Price") == ModelValidationState.Valid 
                && Product.Name.ToLower().StartsWith("small") && Product.Price > 100)
            {
                ModelState.AddModelError("", "Small products cannot cost more than $100");
            }

            if (ModelState.GetValidationState("Product.CategoryId") == ModelValidationState.Valid
               && !_dataContext.Categories.Any(c => c.CategoryId == Product.CategoryId))
            {
                ModelState.AddModelError("Product.CategoryId", "Enter an existring categoryId");
            }

            if (ModelState.GetValidationState("Product.SupplierId") == ModelValidationState.Valid
                && !_dataContext.Suppliers.Any(c => c.SupplierId == Product.SupplierId))
            {
                ModelState.AddModelError("Product.SupplierId", "Enter an existring categoryId");
            }

            if (ModelState.IsValid)
            {
                TempData["name"] = Product.Name;
                TempData["price"] = Product.Price.ToString();
                TempData["categoryId"] = Product.CategoryId.ToString();
                TempData["supplierId"] = Product.SupplierId.ToString();

                return RedirectToPage("FormResults");
            }
            else
            {
                return Page();

            }
        }
    }
}
