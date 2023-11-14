using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UsingViewComponents.Interfaces;
using UsingViewComponents.Models;

namespace UsingViewComponents.Components
{
    public class CitySummary : ViewComponent
    {
        private readonly ICityRepository _cityRepository;

        public CitySummary(ICityRepository cityRepository) =>
            _cityRepository = cityRepository;

        //public IViewComponentResult Invoke() {
        //    var cityModel = new CityViewModel {
        //        Cities = _cityRepository.Cities.Count(),
        //        Population = _cityRepository.Cities.Sum(c=>c.Population) 
        //    };

        //    return View(cityModel);
        //}

        //public IViewComponentResult Invoke() {
        //    return Content("This is a <h3><i>string</i></h3>");
        //}

        //public IViewComponentResult Invoke() {
        //    var html = new HtmlString("This is a <h3><i>string</i></h3>");

        //    return new HtmlContentViewComponentResult(html);
        //}

        //public IViewComponentResult Invoke() {
        //    var target = RouteData.Values["id"] as string;
        //    var cities = _cityRepository.Cities.Where(c=>target == null
        //    || string.Compare(c.Country, target, true) == 0);

        //    var cityVm = new CityViewModel
        //    {
        //        Cities = cities.Count(),
        //        Population = cities.Sum(c=>c.Population)
        //    };

        //    return View(cityVm);
        //}

        public IViewComponentResult Invoke(bool showList)
        {
            if (showList)
                return View("CityList", _cityRepository.Cities);
            else
                return View(new CityViewModel
                {
                    Cities = _cityRepository.Cities.Count(),
                    Population = _cityRepository.Cities.Sum(c => c.Population)
                });
        }
    }
}
