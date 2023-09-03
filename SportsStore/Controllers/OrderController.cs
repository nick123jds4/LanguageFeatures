using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Data;
using SportsStore.Models.Entities;
using SportsStore.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Cart _cart;

        public OrderController(IOrderRepository orderRepository, Cart cart)
        {
            _orderRepository = orderRepository;
            _cart = cart;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order) {
            if (_cart.Lines.Count() == 0) {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _orderRepository.SaveOrder(order);

                return RedirectToAction(nameof(Completed));
            }
            else {

                return View(order);
            }
        }

        public ViewResult Completed() { _cart.Clear(); return View(); }

        /// <summary>
        /// Для отображения списка неотгруженных заказов
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ViewResult List() => View(_orderRepository.Orders.Where(o=>!o.Shipped));

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderId) {
            var order = _orderRepository.Orders.FirstOrDefault(o=>o.OrderId==orderId);
            if (order != null) {
                order.Shipped = true;
                _orderRepository.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));

        }
    }
}
