using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Views.Infrastructure
{
    public class ColorExpander : IViewLocationExpander
    {
        private static Dictionary<string, string> Colors = new Dictionary<string, string> { 
        ["red"] = "Red", ["green"] = "Green", ["blue"]="Blue",
        };
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            context.Values.TryGetValue("color", out var color);
            foreach (var location in viewLocations)
            {
                if (!string.IsNullOrEmpty(color))
                    yield return location.Replace("{0}", color);
                else
                    yield return location;
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var routeValues = context.ActionContext.RouteData.Values;
            if (routeValues.ContainsKey("id")
                && Colors.TryGetValue(routeValues["id"] as string, out var color)
                && !string.IsNullOrEmpty(color)) {
                context.Values["color"] = color;
            }
        }
    }
}
