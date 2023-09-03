using SportsStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        /// <summary>
        /// Сохранение продукта в БД
        /// </summary>
        /// <param name="product"></param>
        void SaveProduct(Product product);
        /// <summary>
        /// Удаление продукта из БД
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product DeleteProduct(int productId);
    }
}
