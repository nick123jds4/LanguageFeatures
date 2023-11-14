using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using System;
using System.Collections.Generic;
using UsingViewComponents.Components;
using UsingViewComponents.Interfaces;
using UsingViewComponents.Models;
using Xunit;

namespace UsingViewComponents.Tests
{
    public class SummaryViewComponentTests
    {
        public IList<City> GetCities() {
            return new List<City>{
                new City{ Population = 100},
                new City{ Population = 20000},
                new City{ Population = 1000000},
                new City{ Population = 500000},
            };
        }

        [Fact]
        public void TestSummary()
        { 
            var repository = new Mock<ICityRepository>();
            repository.SetupGet(m=>m.Cities).Returns(GetCities()); 
            var viewComponent = new CitySummary(repository.Object);

            var result = viewComponent.Invoke(false) as ViewViewComponentResult;

            Assert.IsType(typeof(CityViewModel), result.ViewData.Model);
            Assert.Equal(4, ((CityViewModel)result.ViewData.Model).Cities);
            Assert.Equal(1520100, ((CityViewModel)result.ViewData.Model).Population);
        }
    }
}
