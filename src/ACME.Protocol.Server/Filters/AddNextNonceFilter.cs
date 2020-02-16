using ACME.Protocol.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AddNextNonceFilter> _logger;

        public AddNextNonceFilter(INonceService nonceService, ILogger<AddNextNonceFilter> logger)
        {
            _nonceService = nonceService;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var newNonce = await _nonceService.CreateNonceAsync(context.HttpContext.RequestAborted);
            context.HttpContext.Response.Headers.Add("Replay-Nonce", newNonce.Token);

            _logger.LogInformation($"Added Replay-Nonce: {newNonce.Token}");

            await next.Invoke();
        }
    }
}
