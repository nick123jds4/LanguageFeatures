using Filters.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Filter.Tests
{
    public class FilterTests
    {
        [Fact]
        public void TestHttpsFilter()
        {
            var request = new Mock<HttpRequest>(); 
            request.SetupSequence(m => m.IsHttps)
                .Returns(true)
                .Returns(false);

            var httpContext = new Mock<HttpContext>(); 
            httpContext.SetupGet(m=>m.Request).Returns(request.Object);

            var actionContext = new ActionContext(httpContext.Object, new RouteData(), new ActionDescriptor());

            var authContext = new AuthorizationFilterContext(actionContext, Enumerable.Empty<IFilterMetadata>().ToList());
            var sut = new HttpsOnlyAttribute();

            sut.OnAuthorization(authContext);

            Assert.Null(authContext.Result);

            sut.OnAuthorization(authContext);
            Assert.IsType(typeof(StatusCodeResult), authContext.Result);
            Assert.Equal(StatusCodes.Status403Forbidden, (authContext.Result as StatusCodeResult).StatusCode);

        }


        [Fact]
        public void TestHttpsFilter2()
        {

            // Arrange
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.SetupSequence(m => m.IsHttps).Returns(true)
                                                     .Returns(false);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(httpRequest.Object);

            var actionContext = new ActionContext(httpContext.Object,
                new Microsoft.AspNetCore.Routing.RouteData(),
                new ActionDescriptor());
            var authContext = new AuthorizationFilterContext(actionContext,
                Enumerable.Empty<IFilterMetadata>().ToList());

            HttpsOnlyAttribute filter = new HttpsOnlyAttribute();

            // Act and Assert
            filter.OnAuthorization(authContext);
            Assert.Null(authContext.Result);

            filter.OnAuthorization(authContext);
            Assert.IsType(typeof(StatusCodeResult), authContext.Result);
            Assert.Equal(StatusCodes.Status403Forbidden,
                (authContext.Result as StatusCodeResult).StatusCode);
        }

    }
}
