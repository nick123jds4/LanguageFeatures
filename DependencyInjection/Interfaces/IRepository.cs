using DependencyInjection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Interfaces
{
    public interface IRepository
    {
        Product this[string name] { get; }
        IEnumerable<Product> Products { get; }
        void AddProduct(Product product);
        void DeleteProduct(Product product);
    }
}
