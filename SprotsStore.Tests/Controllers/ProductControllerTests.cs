using SportsStore.Controllers;
using SportsStore.Models.Entities;
using SportsStore.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Moq;
using SportsStore.Models.ViewModels;
using System;

namespace SprotsStore.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> _repository;
        private readonly ProductController _controller;
        public ProductControllerTests()
        {
            _repository = new Mock<IProductRepository>();
            _repository.Setup(m => m.Products).Returns(GetProducts().AsQueryable<Product>());
            _controller = new ProductController(_repository.Object) { PageSize = 3 };
        }

        private IEnumerable<Product> GetProducts()
        {
            return new Product[] {
                    new Product { ProductID =1, Name ="P1", Category = "Cat1" },
                    new Product {ProductID =2, Name = "P2", Category = "Cat2" },
                    new Product {ProductID =3, Name = "P3", Category = "Cat1" },
                    new Product {ProductID =4, Name = "P4", Category = "Cat2" },
                    new Product {ProductID =5, Name = "P5", Category = "Cat3" },
            };
        }  

        [Fact]
        public void Can_Paginate()
        { 
            var sut = new ProductController(_repository.Object);
            sut.PageSize = 3;

            var result = sut.List(null, 2).ViewData.Model as ProductsListViewModel;
            var actual = result.Products.ToArray();

            Assert.True(actual.Length == 2);
            Assert.Equal("P4", actual[0].Name);
            Assert.Equal("P5", actual[1].Name);

        }

        [Fact]
        public void Can_Send_Pagination() {  

            var action = _controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            var pageInfo = action.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products() {  
            var actual = (_controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel).Products.ToArray();

            Assert.Equal(2, actual.Length);
            Assert.True(actual[0].Name == "P2" && actual[0].Category == "Cat2");
            Assert.True(actual[1].Name == "P4" && actual[0].Category == "Cat2");
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            Func<ViewResult, ProductsListViewModel> GetModel = result => result?.ViewData?.Model as ProductsListViewModel;

            int? res1 = GetModel(_controller.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(_controller.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(_controller.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(_controller.List(null))?.PagingInfo.TotalItems;

            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
