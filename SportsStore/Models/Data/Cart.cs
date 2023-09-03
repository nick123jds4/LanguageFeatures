using SportsStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Data
{
    /// <summary>
    /// Корзина
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Товары в корзине
        /// </summary>
        private List<CartLine> _lineCollection = new List<CartLine>();
        public virtual IEnumerable<CartLine> Lines => _lineCollection;  

        public virtual void AddItem(Product product, int quantity) {
            var line = _lineCollection
                .Where(p=>p.Product.ProductID == product.ProductID).FirstOrDefault();

            if (line == null) {
                _lineCollection.Add(new CartLine() { Product = product, Quantity = quantity });
            }
            else {
                line.Quantity += quantity;
            } 
        }

        public virtual void RemoveLine(Product product) => _lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID); 

        public virtual void Clear() => _lineCollection.Clear();

        public virtual decimal ComputeTotalValue() => _lineCollection.Sum(p=>p.Product.Price * p.Quantity);

    }
}
