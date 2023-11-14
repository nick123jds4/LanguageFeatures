using Filters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filters.Infrastructure
{
    public class ViewResultDiagnostics : IActionFilter
    {
        private readonly IFilterDiagnostics _diagnostics; 
        public ViewResultDiagnostics(IFilterDiagnostics diagnostics) => 
            _diagnostics = diagnostics;


        public void OnActionExecuting(ActionExecutingContext context)
        { }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            ViewResult viewResult = null;
            if ((viewResult = context.Result as ViewResult) != null) {
                _diagnostics.AddMessage($"view name: {viewResult.ViewName}");
                _diagnostics.AddMessage($"model type: {viewResult.ViewData.Model.GetType().Name}");
            }
        }
    }
}
