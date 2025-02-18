using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.DB;
using WebApp.Models.View;

namespace WebApp.Models
{
    public class EditroPageModel : PageModel
    {
        public DataContext DataContext;

        public EditroPageModel(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IEnumerable<Category> Categories => DataContext.Categories;
        public IEnumerable<Supplier> Suppliers => DataContext.Suppliers;

        public ProductViewModel? ViewModel { get; set; }

        protected async Task CheckNewCategory(Product product)
        {
            if(product.CategoryId == -1 && !string.IsNullOrEmpty(product.Category?.Name))
            {
                DataContext.Categories.Add(product.Category);

                await DataContext.SaveChangesAsync();

                product.CategoryId = product.Category.CategoryId;

                ModelState.Clear();

                TryValidateModel(product);
            }
        }
    }
}