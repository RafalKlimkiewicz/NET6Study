using Microsoft.AspNetCore.Mvc;
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
            var product = await DataContext.Products.FindAsync(id);

            ViewModel = ProductViewModelFactory.Edit(product ?? new Product(), Categories, Suppliers);
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