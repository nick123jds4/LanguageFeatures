using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsingViewComponents.Interfaces;
using UsingViewComponents.Models;

namespace UsingViewComponents.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _repository;

        public HomeController(IProductRepository repository) => _repository = repository;
        public IActionResult Index()
        {
            return View(_repository.Products);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public IActionResult Create(Product product) {
            _repository.AddProduct(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
