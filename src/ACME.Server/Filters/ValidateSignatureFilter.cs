using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Services;
using TGIT.ACME.Server.Extensions;

namespace TGIT.ACME.Server.Filters
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
                var acmeRequest = context.GetAcmeRequest();
                await _validationService.ValidateSignatureAsync(acmeRequest, context.HttpContext.RequestAborted);
            }

            await next();
        }
    }
}