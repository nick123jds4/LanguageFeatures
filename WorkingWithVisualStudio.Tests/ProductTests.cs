using System;
using WorkingWith.Models;
using Xunit;

namespace WorkingWithVisualStudio.Tests
{
    /// <summary>
    /// Run All tests (Ctrl R, A)
    /// </summary>
    public class ProductTests
    {
        [Fact]
        public void CanChangeProductName()
        {
            var sut = new Product() { Name="Test", Price= 100M};

            sut.Name = "New Name";

            Assert.Equal("New Name", sut.Name);
        }

        [Fact]
        public void CanChangeProductPrice()
        {
            var sut = new Product() { Name = "Test", Price = 100M };

            sut.Price = 200M;

            Assert.Equal(200M, sut.Price);
        }
    }
}
