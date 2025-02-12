using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.DB;

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
    }
}