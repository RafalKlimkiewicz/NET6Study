using Microsoft.AspNetCore.Mvc;
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
            ViewModel = ProductViewModelFactory.Create(new Product(), Categories, Suppliers);
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
