using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filters.Infrastructure
{
    public class ViewResultDetailAttribute: ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        { 
            var dict = new Dictionary<string, string>() { 
            ["Result Type"] = context.Result.GetType().Name
            };
            ViewResult result = null;
            if ((result = context.Result as ViewResult) != null) {
                dict["view name"] = result.ViewName;
                dict["model type"] = result.ViewData.Model.GetType().Name;
                dict["model data"] = result.ViewData.Model.ToString();
            }

            context.Result = new ViewResult() {
            ViewName = "Message",
            ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = dict} 
            };

            await next();
        }
    }
}
