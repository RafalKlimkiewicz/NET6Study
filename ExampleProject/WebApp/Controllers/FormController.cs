using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using WebApp.Models.DB;

namespace WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class FormController : Controller
    {
        private DataContext _context;

        public FormController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(long? id)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
            var p = await _context.Products.Include(p => p.Category)
                .Include(p => p.Supplier).FirstOrDefaultAsync(p => id == null || p.ProductId == id);
            
            return View("Form", p);
        }

        [HttpPost]
        public IActionResult SubmitForm(string name, decimal price)
        {
            TempData["name param controler"] = name;
            TempData["price param controler"] = price.ToString();

            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View();
        }
    }
}
