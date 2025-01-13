using Microsoft.AspNetCore.Mvc;
using WebApp.Models.DB;

namespace WebApp.Components
{
    public class CitySummary : ViewComponent
    {
        private CitiesData _citiesData;

        public CitySummary(CitiesData cdata)
        {
            _citiesData = cdata;
        }

        public string Invoke()
        {
            return $"{_citiesData.Cities.Count()} cities, {_citiesData.Cities.Sum(c => c.Population)} people";
        }
    }
}