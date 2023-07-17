using LanguageFeatures.Entities;
using LanguageFeatures.Extentions;
using LanguageFeatures.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        bool FilterByPrice(Product product) =>
            (product?.Price ?? 0) >= 20;

        bool FilterByName(Product product) =>
            product?.Name?[0] == 'S';

        public async Task<ViewResult> Index() {
            var length = await AsynMethods.GetPageLengthAsync();

            return View(new string[]{ $"Length: {length}"});
        }
        //public IActionResult Index()
        //{
        //    //var result = new List<string>();
        //    //foreach (var product in Product.GetProducts())
        //    //{
        //    //    string name = product?.Name??"<No Name>";
        //    //    decimal? price = product?.Price??0;
        //    //    string rName = product?.Related?.Name??"<None>";
        //    //    result.Add($"Name: {name}, Price: {price:C2}, Related: {rName}");
        //    //}

        //    //var cart = new ShoppingCart() { Products = Product.GetProducts()};
        //    //decimal cartTotal = cart.TotalPrices();

        //    Product[] productArray = {
        //        new Product {Name = "Kayak", Price = 275M},
        //        new Product {Name = "Lifejacket", Price = 48.95M},
        //        new Product {Name = "Soccer ball", Price = 19.50M},
        //        new Product {Name = "Corner flag", Price = 34.95M}
        //    };
        //    decimal totalArray = productArray.Filter(p=>(p?.Price??0) >= 20).TotalPrices();
        //    decimal nameFilterTotal = productArray.Filter(p=>p?.Name?[0] == 'S').TotalPrices();

        //    return View(nameof(Index), 
        //        new string[] { 
        //            //$"Total: {cartTotal:C2}",
        //            $"Array Total: {totalArray:C2}",
        //            $"Name Total: {nameFilterTotal:C2}",
        //        });
        //}
    }
}
