using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters.Infrastructure
{
    public class MessageAttribute: ResultFilterAttribute
    {
        private readonly string _message; 
        public MessageAttribute(string message) => _message = message;

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            WriterMessage(context, $"<div>Before result: {_message}</div>");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            WriterMessage(context, $"<div>After result: {_message}</div>");
        }

        private async void WriterMessage(FilterContext context, string message) {
            var bytes = Encoding.ASCII.GetBytes($"<div>{message}</div>");
            await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        
        }
    }
}
