﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Entities
{
    /// <summary>
    /// Представляет товар. выбранный пользователем, 
    /// а также приобретаемое количество товара
    /// </summary>
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; } 
    }
}
