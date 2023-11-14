using DependencyInjection.Interfaces;
using DependencyInjection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Data
{
    public class DictionaryStorage : IModelStorage
    {

        private Dictionary<string, Product> _items = new Dictionary<string, Product>();
        public Product this[string key] { 
            get => _items[key];
            set => _items[key] = value;
        }

        public IEnumerable<Product> Items => _items.Values;

        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        public void RemoveItem(string key)
        {
            _items.Remove(key);
        }
    }
}
