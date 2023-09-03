using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Data;

namespace SportsStore.Components
{
    public class CartSummary: ViewComponent
    {
        private Cart _cart;
        public CartSummary(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke() {
            return View(_cart);
        }
    }
}
