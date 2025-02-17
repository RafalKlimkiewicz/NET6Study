using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.DB;
using WebApp.Models.ModelFactories;

namespace WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        private IEnumerable<Category> Categories => _dataContext.Categories;
        private IEnumerable<Supplier> Suppliers => _dataContext.Suppliers;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Products.Include(p => p.Category).Include(p => p.Supplier));
        }


        public async Task<IActionResult> Details(long id)
        {

            Product? product = await _dataContext.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(p => p.ProductId == id) ?? new Product();

            var model = ProductViewModelFactory.Details(product);

            return View("ProductEditor", model);
        }

        public IActionResult Create()
        {
            return View("ProductEditor", ProductViewModelFactory.Create(new Product(), Categories, Suppliers));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = default;
                product.Category = default;
                product.Supplier = default;

                _dataContext.Products.Add(product);

                await _dataContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View("ProductEditor", ProductViewModelFactory.Create(product, Categories, Suppliers));
        }

        public async Task<IActionResult> Edit(long id)
        {

            Product? product = await _dataContext.Products.FindAsync(id);

            if(product != null)
                return View("ProductEditor", ProductViewModelFactory.Edit(product, Categories, Suppliers));

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Category = default;
                product.Supplier = default;

                _dataContext.Products.Update(product);

                await _dataContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View("ProductEditor", ProductViewModelFactory.Edit(product, Categories, Suppliers));
        }
    }
}