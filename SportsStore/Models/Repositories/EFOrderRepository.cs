using Microsoft.EntityFrameworkCore;
using SportsStore.Models.Entities;
using SportsStore.Models.Interfaces;
using System;
using System.Linq;

namespace SportsStore.Models.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public EFOrderRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);

        /// <summary>
        /// AttachRange: Когда данные корзины пользователя десериализируются из состояния сеанса, пакет JSON создает
        //новые объекты, не известные инфраструктуре Eпtity Framework Core, которая затем пытается
        //записать все объекты в базу данных.В случае объектов Product это означает, что
        //инфраструктура Eпtity Framework Core попытается записать объекты, которые уже были сохранены, что приведет к ошибке.Во избежание проблемы мы уведомляем Eпtity Framework
        //Core о том, что объекты существуют и не должны сохраняться в базе данных до тех пор,
        //пока они не будут модифицированы.
        //В результате инфраструктура Eпtity Framework Core не будет пытаться записывать десериализированные объекты Product, которые ассоциированы с объектом Order.
        /// </summary>
        /// <param name="order"></param>
        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l=>l.Product));
            if (order.OrderId == 0) {
                _context.Orders.Add(order);
            }

            _context.SaveChanges();
        }
    }
}
