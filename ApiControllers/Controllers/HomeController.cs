using ApiControllers.Interfaces;
using ApiControllers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControllers.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository; 
        public HomeController(IRepository repository) => _repository = repository;
        public IActionResult Index()
        {
            return View(_repository.Reservations);
        }

        [HttpPost]
        public IActionResult Add(Reservation reservation){
            _repository.Add(reservation);

            return RedirectToAction("Index");
        }
    }
}
