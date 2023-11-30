using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Users.Infrastructure
{
    public class LocationClaimsProvider : IClaimsTransformation
    {
        /// <summary>
        /// Предоставляет центральную точку преобразования для изменения указанного участника. Примечание. Это будет выполняться при каждом вызове AuthenticateAsync, поэтому безопаснее возвращать новый ClaimsPrincipal, если ваше преобразование не является идемпотентным.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal != null && !principal.HasClaim(c=>c.Type == ClaimTypes.PostalCode)) {
                var claimIdentity = principal.Identity as ClaimsIdentity;
                if (claimIdentity != null && claimIdentity.IsAuthenticated && claimIdentity.Name != null) {
                    if (claimIdentity.Name.ToLower() == "alice")
                    {
                        var claims = new Claim[] {
                        CreateClaim(ClaimTypes.PostalCode, "DC 20500"),
                        CreateClaim(ClaimTypes.StateOrProvince, "DC")
                        };
                        claimIdentity.AddClaims(claims);
                    }
                    else {
                        var claims = new Claim[] {
                        CreateClaim(ClaimTypes.PostalCode, "NY 10036"),
                        CreateClaim(ClaimTypes.StateOrProvince, "NY")
                        };
                        claimIdentity.AddClaims(claims);
                    }
                }
            
            }

            return Task.FromResult(principal);
        }

        private static Claim CreateClaim(string type, string value) =>
            new Claim(type, value, ClaimValueTypes.String, "RemoteClaims");

    }
}
