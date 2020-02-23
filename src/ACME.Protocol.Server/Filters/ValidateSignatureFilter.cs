using ACME.Protocol.Server.Extensions;
using ACME.Protocol.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Filters
{
    public class ValidateSignatureFilter : IAsyncActionFilter
    {
        private readonly IRequestValidationService _validationService;
        private readonly ILogger<ValidateSignatureFilter> _logger;

        public ValidateSignatureFilter(IRequestValidationService signatureService, ILogger<ValidateSignatureFilter> logger)
        {
            _validationService = signatureService;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (HttpMethods.IsPost(context.HttpContext.Request.Method))
            {
                _logger.LogInformation("Attempting to validate Nonce");
                var acmeRequest = context.GetAcmeRequest();

                await _validationService.ValidateSignatureAsync(acmeRequest, context.HttpContext.RequestAborted);
            }

            await next();
        }
    }
}