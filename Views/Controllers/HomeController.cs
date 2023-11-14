using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Views.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("MyView", new string[] { "apple", "orange", "pear"}); 
        }

        public ViewResult List() => View();
    }
}
