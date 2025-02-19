using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApp.Models;
using WebApp.Models.DB;
using WebApp.Models.ModelFactories;

namespace WebApp.Pages
{
    public class CreateModel : EditroPageModel
    {
        public CreateModel(DataContext dataContext) : base(dataContext) { }

        public void OnGet()
        {
            Product p = TempData.ContainsKey("product") ? JsonSerializer.Deserialize<Product>((TempData["product"] as string)!)! : new Product();

            ViewModel = ProductViewModelFactory.Create(p, Categories, Suppliers);
        }

        public async Task<IActionResult> OnPostAsync([FromForm] Product product)
        {
            await CheckNewCategory(product);

            if (ModelState.IsValid)
            {
                product.ProductId = default;
                product.Category = default;
                product.Supplier = default;

                DataContext.Products.Add(product);

                await DataContext.SaveChangesAsync();

                return RedirectToPage(nameof(Index));
            }

            ViewModel =  ProductViewModelFactory.Create(product, Categories, Suppliers);

            return Page();
        }
    }
}
