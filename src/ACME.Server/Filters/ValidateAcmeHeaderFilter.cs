using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TG_IT.ACME.Protocol.Services;
using TG_IT.ACME.Server.Extensions;

namespace TG_IT.ACME.Server.Filters
{
    public class ValidateAcmeHeaderFilter : IAsyncActionFilter
    {
        private readonly IRequestValidationService _validationService;

        public ValidateAcmeHeaderFilter(IRequestValidationService validationService)
        {
            _validationService = validationService;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (HttpMethods.IsPost(context.HttpContext.Request.Method))
            {
                var acmeRequest = context.GetAcmeRequest();
                await _validationService.ValidateRequestHeaderAsync(acmeRequest, context.HttpContext.RequestAborted);
            }

            await next();
        }
    }
}
