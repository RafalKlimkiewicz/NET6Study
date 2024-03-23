using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public ViewResult RsvpForm()
    {
        return View();
    }

    [HttpPost]
    public ViewResult RsvpForm(GuestReponse guestReponse)
    {
        if (ModelState.IsValid)
        {
            Repository.AddResponse(guestReponse);

            return View("Thanks", guestReponse);
        }

        return View();
    }

    public ViewResult ListResponses()
    {
        return View(Repository.Responses.Where(r => r.WillAttend == true));
    }
}
