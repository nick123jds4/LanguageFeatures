using LanguageFeatures.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LanguageFeatures.Extentions
{
    public static class ShoppingCartExtentions
    {
        /// <summary>
        /// Определение общей стоимости объектов Product
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public static decimal TotalPrices(this IEnumerable<Product> products)
        {
            decimal total = 0;
            foreach (var product in products)
            {
                total += product?.Price ?? 0;
            }

            return total;
        }

        //public static IEnumerable<Product> FilterByPrice(this IEnumerable<Product> products, decimal minimumPrice) {
        //    foreach (var product in products)
        //    {
        //        if ((product?.Price ?? 0) >= minimumPrice)
        //            yield return product;
        //    }
        //}

        //public static IEnumerable<Product> FilterByName(this IEnumerable<Product> products, char firstLetter) {
        //    foreach (var product in products)
        //    {
        //        if ((product?.Name?[0] == firstLetter))
        //            yield return product;
        //    }
        //}

        public static IEnumerable<Product> Filter(this IEnumerable<Product> products, Func<Product, bool> selector) {
            foreach (var product in products)
            {
                if (selector(product))
                    yield return product;
            }
        } 


    }
}
