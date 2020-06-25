using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace TGIT.ACME.Server.Security
{
    public class AcmeAuthenticationOptions : AuthenticationSchemeOptions {
        public AcmeAuthenticationOptions()
        {
            ClaimsIssuer = "ACME";
        }
    }

    public class AcmeAuthenticationHandler : AuthenticationHandler<AcmeAuthenticationOptions>
    {
        public AcmeAuthenticationHandler(IOptionsMonitor<AcmeAuthenticationOptions> options, 
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!HttpMethods.IsPost(Request.Method))
                return AuthenticateResult.NoResult();

            

            throw new NotImplementedException();
        }
    }
}
