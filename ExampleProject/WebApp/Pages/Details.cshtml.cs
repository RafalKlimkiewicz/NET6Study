using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.DB;
using WebApp.Models.ModelFactories;

namespace WebApp.Pages
{
    public class DetailsModel : EditroPageModel
    {
        public DetailsModel(DataContext dataContext) : base(dataContext) { }

        public async Task OnGetAsync(long id)
        {
            var p = await DataContext.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(p => p.ProductId == id);

            ViewModel = ProductViewModelFactory.Details(p ?? new Product());
        }
    }
}