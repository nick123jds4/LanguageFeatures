using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ConventionsAndConstraints.Infrastructure
{
    public class UserAgentComparer
    {
        public bool ContainsString(HttpRequest request, string userAgent) =>
            request.Headers["User-Agent"]
                   .Any(header => header.ToLower().Contains(userAgent.ToLower()));
    }
}
