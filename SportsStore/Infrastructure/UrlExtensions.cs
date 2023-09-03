using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure
{
    public static class UrlExtensions
    {
        /// <summary>
        /// генерирует URL, по которому браузер будет возвращаться после обновления корзины, принимая во внимание строку запроса, если она есть.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string PathAndQuery(this HttpRequest request)
        {
            if (request.QueryString.HasValue)
            { 
                return $"{request.Path}{request.QueryString}"; 
            }

            return request.Path.ToString();
        }
    }
}
