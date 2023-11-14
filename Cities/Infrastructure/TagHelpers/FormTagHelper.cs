using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cities.Infrastructure.TagHelpers
{
    public class FormTagHelper: TagHelper
    {
        private readonly IUrlHelperFactory _urlHelper; 
        public FormTagHelper(IUrlHelperFactory factory) => _urlHelper = factory;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContextData { get; set; }
        /// <summary>
        /// для аттрибута <form controller="name"></form>
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// для аттрибута <form action="name"></form>
        /// </summary>
        public string Action { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelper.GetUrlHelper(ViewContextData);
            var action = Action ?? ViewContextData.RouteData.Values["action"].ToString();
            var controller = Controller ?? ViewContextData.RouteData.Values["controller"].ToString();
            var urlContext = new UrlActionContext { Controller=controller, Action = action};
            output.Attributes.SetAttribute("action", urlHelper.Action(urlContext));
        }
    }
}
