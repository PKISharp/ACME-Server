using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using TGIT.ACME.Protocol.HttpModel.Requests;

namespace TGIT.ACME.Server.Extensions
{
    public static class ActionExecutingContextExtensions
    {
        public static AcmePostRequest GetAcmeRequest(this ActionExecutingContext context)
        {
            var acmeRequest = context.ActionArguments.Values
                    .OfType<AcmePostRequest>()
                    .SingleOrDefault();

            if (acmeRequest == null)
                throw new InvalidOperationException("AcmeRequest could not be found in ActionContext.");

            return acmeRequest;
        }
    }
}
