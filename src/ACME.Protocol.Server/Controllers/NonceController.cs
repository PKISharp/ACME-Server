using ACME.Protocol.Server.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Protocol.Server.Controllers
{
    public class NonceController : ControllerBase
    {
        [HttpGet, HttpHead]
        [Route("new-nonce"), IncludeNonceHeader]
        public ActionResult GetNewNonce()
        {
            if (HttpMethods.IsGet(HttpContext.Request.Method))
                return NoContent();
            else
                return Ok();
        }
    }
}
