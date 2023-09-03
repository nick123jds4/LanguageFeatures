using Moq;
using SportsStore.Models.Data;
using SportsStore.Models.Entities;
using SportsStore.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SprotsStore.Tests.Data
{
    public class CartTests
    {
        private readonly Mock<IProductRepository> _repository;
        private readonly Product[] _products;

        public CartTests()
        {
            _repository = new Mock<IProductRepository>();
            _repository.Setup(m => m.Products).Returns(GetProducts().AsQueryable<Product>());
            _products = GetProducts().ToArray();
        }

        private IEnumerable<Product> GetProducts()
        {
            return new Product[] { 
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1", Price = 100m },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2", Price = 50m },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat3" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat4" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat5" },
            };
        }

        [Fact]
        public void Can_Add_New_Lines()
        { 
            var sut = new Cart();
            sut.AddItem(_products[0], 1);
            sut.AddItem(_products[1], 1);

            var result = sut.Lines.ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal(_products[0], result[0].Product);
            Assert.Equal(_products[1], result[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines() {
            var sut = new Cart();
            sut.AddItem(_products[0], 1);
            sut.AddItem(_products[1], 1);
            sut.AddItem(_products[0], 10);

            var results = sut.Lines.OrderBy(c=>c.Product.ProductID).ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);

        }

        [Fact]
        public void Can_Remove_Lines()
        {
            var sut = new Cart();
            sut.AddItem(_products[0], 1);
            sut.AddItem(_products[1], 3);
            sut.AddItem(_products[2], 5);
            sut.AddItem(_products[1], 1);

            sut.RemoveLine(_products[1]); 
             
            Assert.Equal(0, sut.Lines.Count(c =>c.Product == _products[1]));
            Assert.Equal(2, sut.Lines.Count());

        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            var sut = new Cart();
            sut.AddItem(_products[0], 1);
            sut.AddItem(_products[1], 1);
            sut.AddItem(_products[0], 3);

            decimal total = sut.ComputeTotalValue();
             
            Assert.Equal(450M, total);

        }

        [Fact]
        public void Can_Clear_Contents()
        {
            var sut = new Cart();
            sut.AddItem(_products[0], 1);
            sut.AddItem(_products[1], 1);

            sut.Clear();
             
            Assert.Empty(sut.Lines); 
        }
    }
}
