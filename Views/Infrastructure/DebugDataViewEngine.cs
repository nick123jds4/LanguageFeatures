using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Views.Infrastructure
{
    public class DebugDataViewEngine : IViewEngine
    { 
        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            var viewEngine = ViewEngineResult.NotFound(viewPath, new string[] { "(Debug view engine - GetView)"});

            return viewEngine;
        }

        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            if (viewName == "DebugData")
            {
                return ViewEngineResult.Found(viewName, new DebugDataView());
            }
            else {
                return ViewEngineResult.NotFound(viewName, new string[] { "(Debug view engine - FindView)" });
            }
        }
    }
}
