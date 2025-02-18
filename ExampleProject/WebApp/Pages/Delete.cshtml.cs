using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.DB;
using WebApp.Models.ModelFactories;

namespace WebApp.Pages
{
    public class DeleteModel : EditroPageModel
    {
        public DeleteModel(DataContext dataContext) : base(dataContext) { }

        public async Task OnGet(long id)
        {
            var product = await DataContext.Products.FindAsync(id);

            ViewModel = ProductViewModelFactory.Delete(product ?? new Product(), Categories, Suppliers);
        }

        public async Task<IActionResult> OnPostAsync([FromForm] Product product)
        {

            DataContext.Products.Remove(product);

            await DataContext.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}