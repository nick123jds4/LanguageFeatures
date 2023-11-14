//using DependencyInjection.Controllers;
//using DependencyInjection.Infrastructure;
//using DependencyInjection.Interfaces;
//using DependencyInjection.Models;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using Xunit;

//namespace DependencyInjection.Tests
//{
//    public class DI_Tests
//    {
//        [Fact]
//        public void ControllerTest()
//        {
//            var data = new[] { new Product { Name = "Test", Price = 100} };
//            var mock = new Mock<IRepository>();
//            mock.SetupGet(m=>m.Products).Returns(data);

//            var sut = new HomeController(mock.Object);

//            var actual = sut.Index() as ViewResult;

//            Assert.Equal(data, actual.ViewData.Model);
//        }

//        [Fact]
//        public void Index_Broker_Test()
//        {
//            var data = new[] { new Product { Name = "Test", Price = 100 } };
//            var mock = new Mock<IRepository>();
//            mock.SetupGet(m => m.Products).Returns(data);
//            TypeBroker.SetTestObject(mock.Object);
//            var sut = new HomeController(mock.Object);

//            var actual = sut.Index() as ViewResult;

//            Assert.Equal(data, actual.ViewData.Model);
//        }
//    }
//}
