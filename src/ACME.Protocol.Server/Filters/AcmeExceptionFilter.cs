using ACME.Protocol.Model.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ACME.Protocol.Server.Filters
{
    public class AcmeExceptionFilter : IExceptionFilter
    {
        public AcmeExceptionFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is AcmeException acmeException))
            {
                context.Result = new BadRequestResult();
            }
            else
            {
                context.Result = new BadRequestObjectResult(acmeException.GetHttpError());
            }
        }
    }
}
