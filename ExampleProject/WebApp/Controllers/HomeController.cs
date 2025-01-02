using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.AveragePrice = await _dataContext.Products.AverageAsync(x => x.Price);

            return View(await _dataContext.Products.FindAsync(id));
        }

        public IActionResult List()
        {
            return View(_dataContext.Products);
        }
    }
}
