using Advanced.Database;
using Advanced.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Advanced.Pages
{
    public class IndexModel : PageModel
    {
        private DataContext _context;

        public IEnumerable<Person> People { get; set; } = Enumerable.Empty<Person>();
        public IEnumerable<string> Cities { get; set; } = Enumerable.Empty<string>();

        [FromQuery]
        public string SelectedCity { get; set; } = string.Empty;

        public IndexModel(DataContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            People = _context.People.Include(p => p.Department).Include(p => p.Location);
            Cities = _context.Locations.Select(l => l.City).Distinct();
        }

        public string GetClass(string? city) => SelectedCity == city ? "bg-info text-white" : "";
    }
}