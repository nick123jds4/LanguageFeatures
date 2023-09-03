using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models.Data;
using SportsStore.Models.Interfaces;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _product;
        private Cart _cart;
        public CartController(IProductRepository product, Cart cart)
        {
            _product = product;
            _cart = cart;
        }

        public ViewResult Index(string returnUrl) {
            var cartViewModel = new CartIndexViewModel() {
                Cart = _cart,
                ReturnUrl = returnUrl
            };

            return View(cartViewModel);
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = _product.Products
                .FirstOrDefault(p=>p.ProductID == productId);
            if (product != null) { 
                _cart.AddItem(product, 1); 
            }

            return RedirectToAction("Index", new { returnUrl});
        }

        public RedirectToActionResult RemoveToCart(int productId, string returnUrl)
        {
            var product = _product.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            { 
                _cart.RemoveLine(product); 
            }

            return RedirectToAction("Index", new { returnUrl });
        } 
    }
}


