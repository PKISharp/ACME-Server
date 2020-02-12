using ACME.Protocol.Model.ProtocolServices;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Filters
{
    public class AcmeAttributeFilter : IAsyncActionFilter
    {
        private readonly INonceService _nonceService;

        public AcmeAttributeFilter(INonceService nonceService)
        {
            _nonceService = nonceService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var newNonce = await _nonceService.CreateNonceAsync();
            context.HttpContext.Response.Headers.Add("Replay-Nonce", newNonce);

            await next.Invoke();
        }
    }
}
