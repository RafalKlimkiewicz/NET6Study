using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApp.Models;
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

        public async Task<IActionResult> Index([FromQuery]long? id)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
            var p = await _context.Products.Include(p => p.Category)
                .Include(p => p.Supplier).FirstOrDefaultAsync(p => id == null || p.ProductId == id);
            
            return View("Form", p);
        }

        //[HttpPost]
        //public IActionResult SubmitForm(Product product)
        //{
        //    TempData["product"] = JsonSerializer.Serialize(product);

        //    return RedirectToAction(nameof(Results));
        //}


        //[HttpPost]
        //public IActionResult SubmitForm([Bind(Prefix = "Category")] Category category)
        //{
        //    TempData["category"] = JsonSerializer.Serialize(category);

        //    return RedirectToAction(nameof(Results));
        //}

        [HttpPost]
        public IActionResult SubmitForm([Bind("Name", "Category")] Product product)
        {
            TempData["name"] = product.Name;
            TempData["price"] = product.Price.ToString();
            TempData["category name"] = product.Category?.Name;

            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View();
        }

        public string Header([FromHeader(Name = "Accept-Language")] string accept)
        {
            return $"Header: {accept}";
        }
    }
}
