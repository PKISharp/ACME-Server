using ACME.Server.HttpModel.Requests;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace TG_IT.ACME.Server.Extensions
{
    public static class ActionExecutingContextExtensions
    {
        public static AcmeHttpRequest GetAcmeRequest(this ActionExecutingContext context)
        {
            var acmeRequest = context.ActionArguments.Values
                    .OfType<AcmeHttpRequest>()
                    .SingleOrDefault();

            if (acmeRequest == null)
                throw new InvalidOperationException("AcmeRequest could not be found in ActionContext.");

            return acmeRequest;
        }
    }
}
