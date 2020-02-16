using ACME.Protocol.Model.ProtocolServices;
using ACME.Protocol.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Filters
{
    public class AddNextNonceAttribute : ServiceFilterAttribute
    {
        public AddNextNonceAttribute()
            : base(typeof(AddNextNonceFilter))
        { }
    }

    public class AddNextNonceFilter : IAsyncActionFilter
    {
        private readonly INonceService _nonceService;

        public AddNextNonceFilter(INonceService nonceService)
        {
            _nonceService = nonceService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var newNonce = await _nonceService.CreateNonceAsync(context.HttpContext.RequestAborted);
            context.HttpContext.Response.Headers.Add("Replay-Nonce", newNonce.Token);

            await next.Invoke();
        }
    }
}
