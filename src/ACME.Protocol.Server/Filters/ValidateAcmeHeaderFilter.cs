using ACME.Protocol.HttpModel.Requests;
using ACME.Protocol.Model.Exceptions;
using ACME.Protocol.Server.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Filters
{
    public class ValidateAcmeHeaderFilter : IAsyncActionFilter
    {
        private readonly ILogger<ValidateAcmeHeaderFilter> _logger;
        private readonly string[] _supportedAlgs;

        public ValidateAcmeHeaderFilter(ILogger<ValidateAcmeHeaderFilter> logger)
        {
            _logger = logger;

            _supportedAlgs = new[]
            {
                "RS256"
            };
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (HttpMethods.IsPost(context.HttpContext.Request.Method))
            {
                _logger.LogInformation("Attempting to validate AcmeHeader");
                var acmeRequest = context.GetAcmeRequest();
                var acmeHeader = acmeRequest.Header;

                if (!_supportedAlgs.Contains(acmeHeader.Alg))
                    throw new BadSignatureAlgorithmException();

                if (acmeHeader.Jwk != null && acmeHeader.Kid != null)
                    throw new MalformedRequestException("Do not provide both Jwk and Kid.");
                if (acmeHeader.Jwk == null && acmeHeader.Kid == null)
                    throw new MalformedRequestException("Provide either Jwk or Kid.");
            }

            await next();
        }
    }
}
