using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConventionsAndConstraints.Infrastructure
{
    public class UserAgentAttribute : Attribute, IActionConstraintFactory
    {
        private string _substring;
        public UserAgentAttribute(string substring) => _substring = substring;
        public bool IsReusable => false; 
        public IActionConstraint CreateInstance(IServiceProvider services)
        {
            var comparer = services.GetService(typeof(UserAgentComparer)) as UserAgentComparer;

            return new UserAgentConstraint(comparer, _substring);
        }

        private class UserAgentConstraint : IActionConstraint
        {
            private string _substring;
            private UserAgentComparer _userAgentComparer;
            public UserAgentConstraint(UserAgentComparer userAgentComparer, string substring)
            {
                _userAgentComparer = userAgentComparer;
                _substring = substring;
            }

            public int Order { get; set; } = 0; 
            public bool Accept(ActionConstraintContext context)
            {
                var request = context.RouteContext.HttpContext.Request;

                return _userAgentComparer.ContainsString(request, _substring)
                    || context.Candidates.Count() == 1;
            }
        }
    }


    //public class UserAgentAttribute : Attribute, IActionConstraint
    //{
    //    private string _substring; 
    //    public UserAgentAttribute(string substring) => _substring = substring;
    //    public int Order { get; set; } = 0;

    //    public bool Accept(ActionConstraintContext context)
    //    { 
    //        return context
    //            .RouteContext
    //            .HttpContext
    //            .Request.Headers["User-Agent"]
    //            .Any(header=>header.ToLower().Contains(_substring))
    //            || context.Candidates.Count() == 1;
    //    }
    //}
}
