using SportsStore.Models.Entities;
using SportsStore.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public EFProductRepository(DataContext context) => _context = context;
         
        public IQueryable<Product> Products => _context.Products;
         
        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0) {
                _context.Products.Add(product);
            }
            else
            {
                var dbEntry = _context.Products.FirstOrDefault(p=>p.ProductID == product.ProductID);
                if (dbEntry != null) {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            
            _context.SaveChanges(); 
        }
        public Product DeleteProduct(int productId)
        {
            var dbEntry = _context.Products.FirstOrDefault(p=>p.ProductID == productId);
            if (dbEntry != null) {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges(); 
            }

            return dbEntry;
        }
    }
}
