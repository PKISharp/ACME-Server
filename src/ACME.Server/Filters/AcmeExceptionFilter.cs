using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TG_IT.ACME.Protocol.Model.Exceptions;

namespace TG_IT.ACME.Server.Filters
{
    public class AcmeExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<AcmeExceptionFilter> _logger;

        public AcmeExceptionFilter(ILogger<AcmeExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AcmeException acmeException)
            {
                _logger.LogDebug($"Detected {acmeException.GetType()}. Converting to BadRequest.");

                var badRequestResult = new BadRequestObjectResult(acmeException.GetHttpError());
                badRequestResult.ContentTypes.Add("application/problem+json");

                context.Result = badRequestResult;
            }
        }
    }
}
