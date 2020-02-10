using ACME.Protocol.Model.ProtocolServices;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Attributes
{
    public class IncludeNonceHeaderAttribute : ActionFilterAttribute
    {
        private readonly INonceService _nonceService;

        public IncludeNonceHeaderAttribute(INonceService nonceService)
        {
            _nonceService = nonceService;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var newNonce = await _nonceService.CreateNonceAsync();
            context.HttpContext.Response.Headers.Add("Replay-Nonce", newNonce);

            await next.Invoke();
        }
    }
}
