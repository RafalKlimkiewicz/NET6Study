using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    //3
    public class HomeController : Controller
    {
        [RequireHttps]
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller");
        }

        public IActionResult Index2()
        {
            //1.
            if (Request.IsHttps)
                return View("Message", "This is the Index action on the Home controller");
            else
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
        }

        [RequireHttps] //2.
        public IActionResult Secure()
        {
            if (Request.IsHttps)
                return View("Message", "This is the Secure action on the Home controller");
            else
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
        }

        public IActionResult Secure2()
        {
            return View("Message", "This is the Secure action on the Home controller");
        }
    }
}