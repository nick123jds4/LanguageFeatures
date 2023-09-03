using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    /// <summary>
    /// Компонент представления - служит для генерации списка категорий 
    /// </summary>
    public class NavigationMenu: ViewComponent
    {
        private readonly IProductRepository _productRepository;

        public NavigationMenu(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }  
        public IViewComponentResult Invoke() {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            var products = _productRepository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(p=>p);
            
            return View(products);
        }
    }
}
