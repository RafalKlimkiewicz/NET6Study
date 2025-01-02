using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CubedController : Controller
    {
        [TempData]
        public string? Result { get; set; }

        public IActionResult Index()
        {
            return View("Cubed");
        }

        public IActionResult Cube(double num)
        {
            TempData["value"] = num.ToString();
            //TempData["result"] = Math.Pow(num, 3).ToString();
            Result = Math.Pow(num, 3).ToString();

            return RedirectToAction(nameof(Index));
        }
    }
}
