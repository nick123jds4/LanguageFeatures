using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportsStore.Infrastructure;
using SportsStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Data
{
    /// <summary>
    /// службы доступны по всему приложению, 
    /// любой компонент может получать корзину пользователя с применением одного и того же приема.
    /// Этот объект сохраняет себя самостоятельно.
    /// Всегда выдается один и тот же объект.
    /// </summary>
    public class SessionCart: Cart
    {
        [JsonIgnore]
        public ISession Session { get; private set; }

        public static Cart GetCart(IServiceProvider services) {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")??new SessionCart();
            cart.Session = session;

            return cart; 
        }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
