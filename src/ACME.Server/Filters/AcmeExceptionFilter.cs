using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TG_IT.ACME.Protocol.Model.Exceptions;

namespace TG_IT.ACME.Server.Filters
{
    public class AcmeExceptionFilter : IExceptionFilter
    {
        public AcmeExceptionFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AcmeException acmeException)
            {
                var badRequestResult = new BadRequestObjectResult(acmeException.GetHttpError());
                badRequestResult.ContentTypes.Add("application/problem+json");

                context.Result = badRequestResult;
            }
        }
    }
}
