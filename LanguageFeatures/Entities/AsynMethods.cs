using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LanguageFeatures.Entities
{
    public class AsynMethods
    {
        public static Task<long?> GetPageLength()
        {
            HttpClient client = new HttpClient();
            var send = client.GetAsync("http://apress.com");

            return send.ContinueWith((t)=>t.Result.Content.Headers.ContentLength); 
        }

        public static async Task<long?> GetPageLengthAsync() {
            var client = new HttpClient(); 
            var result = await client.GetAsync("http://apress.com");

            return result.Content.Headers.ContentLength;
        }
    }
}
