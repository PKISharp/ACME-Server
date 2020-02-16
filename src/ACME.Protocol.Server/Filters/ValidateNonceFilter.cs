using ACME.Protocol.HttpModel.Requests;
using ACME.Protocol.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Filters
{
    public class ValidateNonceAttribute : ServiceFilterAttribute
    {
        public ValidateNonceAttribute()
            : base(typeof(ValidateNonceFilter))
        { }
    }

    public class ValidateNonceFilter : IAsyncActionFilter
    {
        private readonly INonceService _nonceService;

        public ValidateNonceFilter(INonceService nonceService)
        {
            _nonceService = nonceService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var acmeRequest = context.ActionArguments.Values
                .OfType<AcmeHttpRequest>()
                .SingleOrDefault();

            if (acmeRequest == null)
                throw new InvalidOperationException("Cannot do that here");

            await _nonceService.ValidateNonceAsync(acmeRequest.Header.Nonce, context.HttpContext.RequestAborted);
            await next();
        }
    }
}
