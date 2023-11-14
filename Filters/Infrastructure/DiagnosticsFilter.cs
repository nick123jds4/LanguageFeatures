using Filters.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters.Infrastructure
{
    /// <summary>
    /// Фильтр результатов, пишет данные из инфы в ответ 
    /// </summary>
    public class DiagnosticsFilter : IAsyncResultFilter
    {
        private readonly IFilterDiagnostics _diagnostics; 
        public DiagnosticsFilter(IFilterDiagnostics diagnostics) => 
            _diagnostics = diagnostics;

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await next();
            foreach (var message in _diagnostics?.Messages)
            {
                var bytes = Encoding.ASCII.GetBytes($"<div>{message}</div>");
                await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}
