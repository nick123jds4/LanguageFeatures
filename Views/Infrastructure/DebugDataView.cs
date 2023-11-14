using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.Infrastructure
{
    public class DebugDataView : IView
    {
        public string Path => string.Empty;

        public async Task RenderAsync(ViewContext context)
        {
            context.HttpContext.Response.ContentType = "text/plain";
            var sb = new StringBuilder();
            sb.AppendLine("--Routing data/Маршрут данных--");
            foreach (var item in context.RouteData.Values)
            {
                sb.AppendLine($"Key: {item.Key}, Value: {item.Value}");
            }

            sb.AppendLine("--View Data/Данные представления--");
            foreach (var item in context.ViewData)
            {
                sb.AppendLine($"Key: {item.Key}, Value: {item.Value}");
            }

            await context.Writer.WriteAsync(sb.ToString());
        }
    }
}
