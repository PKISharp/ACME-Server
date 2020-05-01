using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TG_IT.ACME.Server.Filters
{
    public class AcmeIndexLinkFilter : IActionFilter
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public AcmeIndexLinkFilter(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(context);

            var linkHeaderUrl = urlHelper.RouteUrl("Directory", null, "https");
            var linkHeader = $"<{linkHeaderUrl}>;rel=\"index\"";

            context.HttpContext.Response.Headers.Add("Link", linkHeader);
        }
    }
}
