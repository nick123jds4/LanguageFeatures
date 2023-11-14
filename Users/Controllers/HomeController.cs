using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var dict = new Dictionary<string, object>{
            ["Placeholder"] = "PlaceHolder_Value"
            };

            return View(dict);
        }
    }
}
