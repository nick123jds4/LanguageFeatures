using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsStore.Controllers;
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
    public class AdminControllerTests
    {
        private readonly Mock<IProductRepository> _repository;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            _repository = new Mock<IProductRepository>();
            _repository.Setup(m => m.Products).Returns(GetProducts().AsQueryable<Product>());
            _controller = new AdminController(_repository.Object);
        }

        private IEnumerable<Product> GetProducts()
        {
            return new List<Product> {
                    new Product { ProductID =1, Name ="P1", Category = "Cat1" },
                    new Product {ProductID =2, Name = "P2", Category = "Cat2" },
                    new Product {ProductID =3, Name = "P3", Category = "Cat1" },
                    new Product {ProductID =4, Name = "P4", Category = "Cat2" },
                    new Product {ProductID =5, Name = "P5", Category = "Cat3" },
            };
        }

        private T GetViewModel<T>(IActionResult result) where T: class {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        [Fact]
        public void Index_Contains_All_Products() {
            var products = GetViewModel<IEnumerable<Product>>(_controller.Index())?.ToArray();

            Assert.Equal(5, products.Length);
            Assert.Equal("P1", products[0].Name);
            Assert.Equal("P2", products[1].Name);
        }

        [Fact]
        public void Can_Edit_Product() {
            var p1 = GetViewModel<Product>(_controller.Edit(1));
            var p2 = GetViewModel<Product>(_controller.Edit(2));
            var p3 = GetViewModel<Product>(_controller.Edit(3));

            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product() {
            var product = GetViewModel<Product>(_controller.Edit(10));

            Assert.Null(product);
        }

        [Fact]
        public void Can_Save_Valid_Change() {
            var tempData = new Mock<ITempDataDictionary>();
            var target = new AdminController(_repository.Object) {
                TempData = tempData.Object
            };
            var product = GetProducts().First();
            var result = target.Edit(product);

            _repository.Verify(r=>r.SaveProduct(product));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes() {
            _controller.ModelState.AddModelError("error", "error");
            var product = GetProducts().First();
            var result = _controller.Edit(product);

            _repository.Verify(r => r.SaveProduct(It.IsAny<Product>()), Times.Never);
            Assert.IsType<ViewResult>(result);  
        }

        [Fact]
        public void Can_Delete_Valid_Products() {
            var products = GetProducts().ToList();
            var prod = new Product() { ProductID = 7, Name="Test"};
            products.Add(prod);
            _repository.Setup(r=>r.Products).Returns(products.AsQueryable<Product>());

            var controller = new AdminController(_repository.Object);
            controller.Delete(prod.ProductID);

            _repository.Verify(r=>r.DeleteProduct(prod.ProductID));
        }
    }
}
