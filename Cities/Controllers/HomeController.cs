using Cities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cities.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository; 
        public HomeController(IRepository repository) => _repository = repository;

        public IActionResult Index()
        {
            return View(_repository.Cities);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public IActionResult Create(City city) {
            _repository.AddCity(city);

            return RedirectToAction(nameof(Index));
        }
    }
}
