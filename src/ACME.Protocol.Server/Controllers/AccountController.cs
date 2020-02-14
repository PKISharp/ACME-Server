using ACME.Protocol.HttpModel.Requests;
using ACME.Protocol.Server.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Controllers
{
    [ApiController]
    [AddNextNonce]
    public class AccountController : ControllerBase
    {
        [Route("/new-account", Name = "NewAccount")]
        [HttpPost]
        public async Task<ActionResult> CreateOrGetAccount(AcmeHttpRequest<CreateOrGetAccount> request)
        {
            return Ok();
        }
    }
}
