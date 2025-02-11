﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Filters;

namespace WebApp.Controllers
{
    //3. Attribute class
    //[RequireHttps]
    //4.Filter Attribute
    //[HttpsOnly] //wierd error / redirect
    //[RequireHttps]
    //[ResultDiagnostics]
    //[ServiceFilter(typeof(GuidResponseAttribute))] // without IFilterFactory
    //[GuidResponse]
    //[GuidResponse]
    [Message("This is the controller-scoped filter", Order = 10)]
    public class HomeController : Controller
    {
        [Message("This is the first action-scoped filter", Order = 1)]
        [Message("This is the second action-scoped filter", Order = -1)]
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

        //[RequireHttps] //2. attribute method
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

        [ChangeArg]
        public IActionResult Messages(string message1, string message2 = "None")
        {
            return View("Message", $"{message1}, {message2}");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("message1"))
            {
                context.ActionArguments["message1"] = "New message";
            }
        }

        [RangeException]
        public ViewResult GenerateException(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            else if (id > 10)
                throw new ArgumentOutOfRangeException(nameof(id));
            else
                return View("Message", $"The value is{id}");
        }
    }
}