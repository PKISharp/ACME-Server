using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Services;
using TGIT.ACME.Server.Extensions;

namespace TGIT.ACME.Server.Filters
{
    public class ValidateNonceFilter : IAsyncActionFilter
    {
        private readonly IRequestValidationService _validationService;
        private readonly ILogger<ValidateNonceFilter> _logger;

        public ValidateNonceFilter(IRequestValidationService validationService, ILogger<ValidateNonceFilter> logger)
        {
            _validationService = validationService;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (HttpMethods.IsPost(context.HttpContext.Request.Method))
            {
                var acmeRequest = context.GetAcmeRequest();
                await _validationService.ValidateNonceAsync(acmeRequest.Header.Value.Nonce, context.HttpContext.RequestAborted);
            }

            await next();
        }
    }
}
