using ACME.Server.Extensions;
using ACME.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TG_IT.ACME.Server.Filters
{
    public class ValidateAcmeHeaderFilter : IAsyncActionFilter
    {
        private readonly IRequestValidationService _validationService;
        private readonly ILogger<ValidateAcmeHeaderFilter> _logger;

        public ValidateAcmeHeaderFilter(IRequestValidationService validationService, ILogger<ValidateAcmeHeaderFilter> logger)
        {
            _validationService = validationService;
            _logger = logger;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (HttpMethods.IsPost(context.HttpContext.Request.Method))
            {
                _logger.LogInformation("Attempting to validate Nonce");
                var acmeRequest = context.GetAcmeRequest();

                await _validationService.ValidateRequestHeaderAsync(acmeRequest, context.HttpContext.RequestAborted);
            }

            await next();
        }
    }
}
