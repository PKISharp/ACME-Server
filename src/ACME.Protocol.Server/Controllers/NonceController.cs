using ACME.Protocol.Server.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Protocol.Server.Controllers
{
    public class NonceController : ControllerBase
    {
        [HttpGet, HttpHead]
        [Route("/new-nonce", Name = "NewNonce")]
        [AddNextNonce]
        public ActionResult GetNewNonce()
        {
            if (HttpMethods.IsGet(HttpContext.Request.Method))
                return NoContent();
            else
                return Ok();
        }
    }
}
