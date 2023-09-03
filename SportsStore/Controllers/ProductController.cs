using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Data;
using SportsStore.Models.Interfaces;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize { get; set; } = 4;
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository product) => _repository = product;
   
        public ViewResult List(string category, int productPage = 1)
        {
            //var controller = RouteData.Values["controller"].ToString();
            //var action = RouteData.Values["action"].ToString();  
            var products = _repository.Products
                .Where(p=>category == null||p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage- 1) * PageSize).Take(PageSize);

            var pagingInfo = new PagingInfo() {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ? _repository.Products.Count() : _repository.Products.Where(p => p.Category == category).Count()
            };
             
            var viewModel = new ProductsListViewModel() {
                Products = products,
                PagingInfo = pagingInfo,
                CurretnCategory = category
            };

            return View(viewModel);
        }

        public void Test() {
       
        }
    }
}
