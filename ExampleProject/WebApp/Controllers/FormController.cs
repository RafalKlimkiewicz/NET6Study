using Microsoft.AspNetCore.Mvc;
using WebApp.Models.DB;

namespace WebApp.Controllers
{
    public class FormController : Controller
    {
        private DataContext _context;

        public FormController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(long id = 1)
        {
            return View("Form", await _context.Products.FindAsync(id));
        }

        [HttpPost]
        public IActionResult SubmitForm()
        {
            foreach (var key in Request.Form.Keys.Where(k => !k.StartsWith("_")))
                TempData[key] = string.Join(", ", Request.Form[key]);

            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View();
        }
    }
}
