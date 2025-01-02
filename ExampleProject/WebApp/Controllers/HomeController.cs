using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index(long id = 1)
        {
            Product? product = await _dataContext.Products.FindAsync(id);
            
            if(product?.CategoryId == 1)
                return View("Watersports", product);
            else
                return View(product);
        }

        public IActionResult Common()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(_dataContext.Products);
        }
    }
}
