using ACME.Protocol.Model.Exceptions;
using ACME.Protocol.Model.ProtocolServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Filters
{
    public class AcmeExceptionFilter : IExceptionFilter
    {
        private readonly INonceService _nonceService;

        public AcmeExceptionFilter(INonceService nonceService)
        {
            _nonceService = nonceService;
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is AcmeException acmeException))
            {
                //TODO Create generic exception
            }
            else
            {
                context.Result = new BadRequestObjectResult(acmeException.GetHttpError());
            }
        }
    }
}
