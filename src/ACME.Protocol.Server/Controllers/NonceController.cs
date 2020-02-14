using ACME.Protocol.Server.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Protocol.Server.Controllers
{
    [ApiController]
    [AddNextNonce]
    public class NonceController : ControllerBase
    {
        [Route("/new-nonce", Name = "NewNonce")]
        [HttpGet, HttpHead]
        public ActionResult GetNewNonce()
        {
            if (HttpMethods.IsGet(HttpContext.Request.Method))
                return NoContent();
            else
                return Ok();
        }
    }
}
