using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        public ViewResult Index()
        {
            return View("Result", $"This is a derived controller");
        }

        public ViewResult Headers() {
            var headers = Request.Headers.ToDictionary(h=>h.Key, h=>h.Value.FirstOrDefault());

            return View("DictionaryResult", headers);
        }
    }
}
