using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cities.Infrastructure.TagHelpers
{
    [HtmlTargetElement("formbutton")]
    public class FormButtonTagHelper : TagHelper
    {
        public override int Order => 2;
        public string Type { get; set; } = "Submit";
        public string BgColor { get; set; } = "primary";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //если не назначить HTML тег, то будет <formbutton>
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;//<button></button>
            output.Attributes.SetAttribute("class", $"btn btn-{BgColor}");
            //если не назначить свойство для аттрибута, то значение св-ва Type проигнорируется
            output.Attributes.SetAttribute("type", Type);
            output.Content.SetContent(Type == "submit"?"Add":"Reset");

        }
    }
}
