using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWith.Models
{
    public class SimpleRepository
    {
        private static SimpleRepository _sharedRepository = new SimpleRepository();
        public static SimpleRepository SharedRepository => _sharedRepository;

        private Dictionary<string, Product> _products = new Dictionary<string, Product>();
        public IEnumerable<Product> Products => _products.Values;
         
        public SimpleRepository()
        {
            var products = new[] {
            new Product { Name ="Kayak", Price = 275M } ,
            new Product { Name ="Lifejacket", Price = 48.95M },
            new Product { Name ="Soccer ball", Price = 19.50M } ,
            new Product { Name ="Corner flag", Price = 34.95M },
            };

            foreach (var product in products)
            {
                AddProduct(product);
            }

            _products.Add("Error", null);
        }

        public void AddProduct(Product product) => _products.Add(product.Name, product);
    } 
}
