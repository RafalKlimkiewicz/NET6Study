using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApp.Models;
using WebApp.Models.DB;
using WebApp.Models.ModelFactories;

namespace WebApp.Pages
{
    public class EditModel : EditroPageModel
    {
        public EditModel(DataContext dataContext) : base(dataContext) { }

        public async Task OnGet(long id)
        {
            Product? p = TempData.ContainsKey("product") ? JsonSerializer.Deserialize<Product>((TempData["product"] as string)!)! : await DataContext.Products.FindAsync(id);

            ViewModel = ProductViewModelFactory.Edit(p ?? new Product(), Categories, Suppliers);
        }

        public async Task<IActionResult> OnPostAsync([FromForm] Product product)
        {
            await CheckNewCategory(product);

            if (ModelState.IsValid)
            {
                product.Category = default;
                product.Supplier = default;

                DataContext.Products.Update(product);

                await DataContext.SaveChangesAsync();

                return RedirectToPage(nameof(Index));
            }

            ViewModel =  ProductViewModelFactory.Edit(product, Categories, Suppliers);

            return Page();
        }
    }
}