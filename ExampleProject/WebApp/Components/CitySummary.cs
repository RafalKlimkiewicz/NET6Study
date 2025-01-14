using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebApp.Models;
using WebApp.Models.DB;

namespace WebApp.Components
{
    public class CitySummary : ViewComponent
    {
        private CitiesDataContext _citiesData;

        public CitySummary(CitiesDataContext cdata)
        {
            _citiesData = cdata;
        }

        //IViewComponentResult
        public IViewComponentResult Invoke(string themeName = "success")
        {
            ViewBag.Theme = themeName;

            //if (RouteData.Values["controller"] != null)
            //    return "Controller Requset";,
            //else
            //    return "Razor PAge Request";

            //return new HtmlContentViewComponentResult(new HtmlString("This is a <h3><i>string</i></h3>"));

            //return Content("This is a <h3><i>string</i></h3>");

            return View(new CityViewModel()
            {
                Cities = _citiesData.Cities.Count(),
                Population = _citiesData.Cities.Sum(c => c.Population),
            });

            //return $"{_citiesData.Cities.Count()} cities, {_citiesData.Cities.Sum(c => c.Population)} people";
        }
    }
}