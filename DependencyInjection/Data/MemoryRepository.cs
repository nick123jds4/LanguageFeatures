using DependencyInjection.Interfaces;
using DependencyInjection.Models;
using System;
using System.Collections.Generic;

namespace DependencyInjection.Data
{
    public class MemoryRepository : IRepository
    {
        private string _guid = Guid.NewGuid().ToString();
        private IModelStorage _storage; 
        public MemoryRepository(IModelStorage storage)
        {
            _storage = storage;
            new Dictionary<string, Product>();
            new List<Product> {
            new Product{ Name = "Kayak", Price = 275M },
            new Product{ Name = "LifeJacket", Price = 48.95M },
            new Product{ Name = "Soccer ball", Price = 19.50M }, 
            }.ForEach(p => AddProduct(p));
        }

        public Product this[string name] => _storage[name];

        public IEnumerable<Product> Products => _storage.Items;

        public void AddProduct(Product product)
        {
            _storage[product.Name] = product;
        }

        public void DeleteProduct(Product product)
        {
            _storage.RemoveItem(product.Name);
        }

        public override string ToString()
        {
            return _guid;
        }
    }
}
