using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Entities;
using SportsStore.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _product;

        public AdminController(IProductRepository repo) => _product = repo;
        public IActionResult Index()
        {
            return View(_product.Products);
        }

        public ViewResult Edit(int productId) {
            var product = _product.Products
                .FirstOrDefault(p=>p.ProductID == productId);

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product) {
            if (ModelState.IsValid)
            {
                _product.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";

                return RedirectToAction("Index");
            }
            else {
                //Что-то не так с данными
                return View(product);
            }
        }

        /// <summary>
        /// Создать новый продукт
        /// </summary>
        /// <returns></returns>
        public ViewResult Create() => View("Edit", new Product());

        /// <summary>
        /// Метод действия Delete () должен поддерживать только запросы POST, 
        /// потому что удаление объектов не является идемпотентной операцией. Браузеры и кеши вольны выдавать запросы GET без явного согласия пользователя, поэтому мы должны проявить осторожность, чтобы избежать внесения изменений как следствия запросов GET.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int productId) {
            var product = _product.DeleteProduct(productId);
            if (product != null) {
                TempData["message"] = $"{product.Name} was deleted";
            }

            return RedirectToAction("Index");
        }
    }
}
