using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Infrastructure;
using SportsStore.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SprotsStore.Tests.Infrastructure
{
    public class PageLinkTagHelperTests
    {
        [Fact]
        public void Can_Generate_Page_Links() {
            var urlHelper = new Moq.Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1").Returns("Test/Page2").Returns("Test/Page3");

            var factory = new Mock<IUrlHelperFactory>();
            factory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlHelper.Object);

            var helper = new PageLinkTagHelper(factory.Object);
            helper.PageModel = new PagingInfo() {
                CurrentPage = 2,
                TotalItems = 28, 
                ItemsPerPage = 10
            };
            helper.PageAction = "Test";

            var tagHelper = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), string.Empty);

            var content = new Mock<TagHelperContent>();
            var output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(content.Object));

            helper.Process(tagHelper, output);

            Assert.Equal(
                @"<a href=""Test/Page1"">1</a>"+
                @"<a href=""Test/Page2"">2</a>"+
                @"<a href=""Test/Page3"">3</a>", 
                output.Content.GetContent());
        }
    }
}
