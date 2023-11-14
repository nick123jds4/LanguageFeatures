using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UsingViewComponents.Components
{
    public class PageSize: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() {
            var client = new HttpClient();
            var response = await client.GetAsync("http://apress.com");

            return View(response.Content.Headers.ContentLength);
        }
    }
}
