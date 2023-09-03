using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models.Data;
using SportsStore.Models.Entities;
using SportsStore.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SprotsStore.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderRepository> _mock;

        public OrderControllerTests()
        {
            _mock = new Mock<IOrderRepository>(); 
        }

        [Fact]
        public void Cannot_Checkout_Empty_Cart() {
            var cart = new Cart();
            var order = new Order();

            var target = new OrderController(_mock.Object, cart);

            var result = target.Checkout(order) as ViewResult;
            _mock.Verify(m=>m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails() {
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var target = new OrderController(_mock.Object, cart);
            target.ModelState.AddModelError("error", "error");
            var result = target.Checkout(new Order()) as ViewResult;
            _mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid); 
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order() {
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var target = new OrderController(_mock.Object, cart);
            var result = target.Checkout(new Order()) as RedirectToActionResult;
            _mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

            Assert.Equal("Completed", result.ActionName); 
        }
    }
}
