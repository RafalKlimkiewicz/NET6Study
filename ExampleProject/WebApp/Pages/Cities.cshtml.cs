using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Pages
{
    [ViewComponent(Name = "CitiesPageHybrid")]
    public class CitiesModel : PageModel
    {
        [ViewComponentContext]
        public ViewComponentContext Context { get; set; } = new();

        public CitiesDataContext? Data { get; set; }

        public CitiesModel(CitiesDataContext data)
        {
            Data = data;
        }

        public IViewComponentResult Invoke()
        {
            return new ViewViewComponentResult()
            {
                ViewData = new ViewDataDictionary<CityViewModel>(Context.ViewData, new CityViewModel
                {
                    Cities = Data?.Cities.Count(),
                    Population = Data?.Cities.Sum(c => c.Population)
                })
            };
        }
    }
}
