using Microsoft.AspNetCore.Mvc;
using WebApplication_Party5.Models;

namespace WebApplication_Party5.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var product = new Product()
            {
                ProductId = 1,
                Name = "Kayak",
                Description = "А boat for one person",
                Category = "Watersports",
                Price = 275M
            };

            ViewBag.StockLevel = 2;

            return View(product);
        }
    }
}
