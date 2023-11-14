using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cities.Infrastructure.TagHelpers
{
    /// <summary>
    /// Такой подход к управлению включением содержимого нельзя
    //считать приемлемым в реальном приложении
    /// </summary>
    [HtmlTargetElement(Attributes ="show-for-action")]
    public class SelectiveTagHelper: TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContextData { get; set; }
        public string ShowForAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(!ViewContextData.RouteData.Values["action"].ToString().Equals(ShowForAction, StringComparison.OrdinalIgnoreCase))
            {

                output.SuppressOutput();
            }
        }
    }
}
