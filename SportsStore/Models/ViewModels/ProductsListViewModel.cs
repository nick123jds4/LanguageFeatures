using SportsStore.Models.Data;
using SportsStore.Models.Entities;
using System.Collections.Generic;

namespace SportsStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        /// <summary>
        /// Модель сущности <see cref="Product"/>
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
        /// <summary>
        /// Пагинация страниц по продуктам
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
        /// <summary>
        /// Текущая категория
        /// </summary>
        public string CurretnCategory { get; set; }
    }
}
