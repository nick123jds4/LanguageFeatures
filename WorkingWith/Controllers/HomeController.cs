using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingWith.Models;

namespace WorkingWith.Controllers
{
    public class HomeController : Controller
    {
        private SimpleRepository _repository = new SimpleRepository();
        public IActionResult Index()
        {
            return View(_repository.Products.Where(p=>p?.Price < 50));
        }

        [HttpGet]
        public IActionResult AddProduct() => View(new Product());

        [HttpPost]
        public IActionResult AddProduct(Product product) {
            _repository.AddProduct(product);

            return RedirectToAction(nameof(Index));
        }

    }
}
