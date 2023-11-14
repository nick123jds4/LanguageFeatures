using DependencyInjection.Data;
using DependencyInjection.Infrastructure;
using DependencyInjection.Interfaces;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        /// <summary>
        /// репозиторий полученый через брокер типов
        /// </summary>
        public IRepository Repository { get; set; } = TypeBroker.Repository;

        private readonly ProductTotalizer _totalizer;

        public HomeController(IRepository repository, ProductTotalizer totalizer)
        {
            _repository = repository;
            _totalizer = totalizer;
        }

        public IActionResult Index([FromServices]ProductTotalizer totalizer)
        {
            //локатор служб
            var repository = HttpContext.RequestServices.GetService(typeof(IRepository));

            ViewBag.HomeController = _repository.ToString();
            ViewBag.Totalizer = totalizer.Repository.ToString();
            ViewBag.Total = _totalizer.Total;

            return View(_repository.Products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
