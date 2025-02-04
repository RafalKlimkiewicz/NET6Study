using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    //3. Attribute class
    //[RequireHttps]
    //4.Filter Attribute
    [HttpsOnly] //wierd error / redirect
    public class HomeController : Controller
    {
        [RequireHttps]
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller");
        }

        public IActionResult Index2()
        {
            //1. check inside function
            if (Request.IsHttps)
                return View("Message", "This is the Index action on the Home controller");
            else
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
        }

        [RequireHttps] //2. attribute method
        public IActionResult Secure()
        {
            return View("Message", "This is the Secure action on the Home controller");
        }

        public IActionResult Secure2()
        {
            if (Request.IsHttps)
                return View("Message", "This is the Secure action on the Home controller");
            else
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}