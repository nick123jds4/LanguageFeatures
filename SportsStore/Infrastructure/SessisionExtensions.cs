using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    /// <summary>
    /// Средство состояния сеанса в ASP.NET Core хранит только значения int, string и byte[]
    /// </summary>
    public static class SessisionExtensions
    {
        /// <summary>
        /// Cериализирует объект Cart и добавляет его в состояние сеанса
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetJson(this ISession session, string key, object value) {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Для извлечения объекта Cart
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetJson<T>(this ISession session, string key) {
            var data = session.GetString(key);

            return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }

    }
}
