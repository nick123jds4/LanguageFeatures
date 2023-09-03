using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Models.Entities;
using SportsStore.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SprotsStore.Tests.Components
{
    public class NavigationMenuViewComponentTests
    {
        private readonly Mock<IProductRepository> _repository;
        public NavigationMenuViewComponentTests()
        {
            _repository = new Mock<IProductRepository>();
            _repository.Setup(m => m.Products).Returns(GetProducts().AsQueryable<Product>());

        }

        private IEnumerable<Product> GetProducts()
        {
            return new Product[] {
                    new Product { ProductID =1, Name ="P1", Category = "Apples" },
                    new Product {ProductID =2, Name = "P2", Category = "Apples" },
                    new Product {ProductID =4, Name = "P4", Category = "Oranges" },
                    new Product {ProductID =3, Name = "P3", Category = "Plums" },
            };
        }

        [Fact]
        public void Can_Select_Categories()
        {
            var sut = new NavigationMenu(_repository.Object);

            var products = (sut.Invoke() as ViewViewComponentResult).ViewData.Model;
            var result = ((IEnumerable<string>)products).ToArray();

            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, result));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            var categoryToSelect = "Apples";
            var sut = new NavigationMenu(_repository.Object);
            sut.ViewComponentContext = new ViewComponentContext()
            {
                ViewContext = new ViewContext()
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };
            sut.RouteData.Values["category"] = categoryToSelect;

            var result = (string)(sut.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            Assert.Equal(categoryToSelect, result);
        }
    }
}
