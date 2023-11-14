using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {  
        public ViewResult Index()
        {
            var result = new Result() { 
            Controller = nameof(HomeController),
            Action = nameof(Index)
            };

            return View("Result", result);
        } 
    }
}
