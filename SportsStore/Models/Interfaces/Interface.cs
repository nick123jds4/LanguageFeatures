using SportsStore.Models.Entities;
using System.Linq;

namespace SportsStore.Models.Interfaces
{
    /// <summary>
    /// Доступ к объектам заказа
    /// </summary>
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
