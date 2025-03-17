using Advanced.Database;
using Advanced.Services;
using Advanced.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Advanced.Controllers
{
    public class HomeController : Controller
    {
        private DataContext _context;
        private ToggleService _toggleService;

        public HomeController(DataContext context, ToggleService toggleService)
        {
            _context = context;
            _toggleService = toggleService;
        }

        public string Toggle() => $"Enabled: {_toggleService.ToggleComponents()}";

        public IActionResult Index([FromQuery] string selectedCity)
        {
            return View(new PeopleListViewModel
            {
                People = _context.People.Include(p => p.Department).Include(p => p.Location),
                Cities = _context.Locations.Select(l => l.City).Distinct(),
                SelectedCity = selectedCity
            });
        }
    }
}