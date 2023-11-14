using Filters.Infrastructure;
using Filters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Filters.Controllers
{
    //[RequireHttps]
    //[HttpsOnly]
    //[Profile]
    //[ViewResultDetail]
    //[RangeException]
    //[TypeFilter(typeof(DiagnosticsFilter), Order =9)] 
    //[ServiceFilter(typeof(TimeFilter), Order =10)]
    [Message("This is the Controller-scoped filter", Order = 10)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Message("This is the first action-scoped filter", Order =1)]
        [Message("This is the second action-scoped filter", Order =-1)]
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller");
        }
         
        public IActionResult SecondAction()
        { 
            return View("Message", "This is the SecondAction action on the Home controller"); 
        }

        public ViewResult GenerateException(int? id) {
            if (id is null)
                throw new ArgumentNullException(nameof(id));
            else if (id > 10)
                throw new ArgumentOutOfRangeException(nameof(id));
            else
                return View("Message", $"The value is {id}");
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
