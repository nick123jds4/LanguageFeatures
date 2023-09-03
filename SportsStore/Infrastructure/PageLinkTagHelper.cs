using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    /// <summary>
    /// В папку Infrastructure будут помещаться классы, которые 
    /// предоставляют приложению связующий код, но не имеют 
    /// отношения к предметной области приложения. 
    /// </summary>
    [HtmlTargetElement("div", Attributes ="page-model")]
    public class PageLinkTagHelper:TagHelper
    {
        private readonly IUrlHelperFactory _factory;

        public PageLinkTagHelper(IUrlHelperFactory factory)
        {
            _factory = factory;
            PageUrlValues = new Dictionary<string, object>();
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } 
        public PagingInfo PageModel { get; set; } 
        public string PageAction { get; set; }

        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _factory.GetUrlHelper(ViewContext);
            var builderDiv = new TagBuilder("div");
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                PageUrlValues["productPage"] = i;
                var builderA = new TagBuilder("a");
                builderA.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                builderA.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i});
                if (PageClassesEnabled) {
                    builderA.AddCssClass(PageClass);
                    builderA.AddCssClass(i==PageModel.CurrentPage?PageClassSelected:PageClassNormal);
                }

                builderA.InnerHtml.Append(i.ToString());
                builderDiv.InnerHtml.AppendHtml(builderA); 
            }

            output.Content.AppendHtml(builderDiv.InnerHtml);
        }


    }
}
