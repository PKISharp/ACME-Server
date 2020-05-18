using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Services;
using TGIT.ACME.Server.Extensions;

namespace TGIT.ACME.Server.Filters
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
