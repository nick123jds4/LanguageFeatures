using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using UsingViewComponents.Interfaces;
using UsingViewComponents.Models;

namespace UsingViewComponents.Controllers
{
    [ViewComponent(Name = "ComboComponent")]
    public class CityController : Controller
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository) =>
            _cityRepository = cityRepository;

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(City city)
        {
            _cityRepository.AddCity(city);

            return RedirectToAction("Index", "Home");
        }

        public IViewComponentResult Invoke()
        {

            return new ViewViewComponentResult()
            {
                ViewData = new ViewDataDictionary<IEnumerable<City>>(ViewData, _cityRepository.Cities)

            };
        }
    }
}
