using ACME.Protocol.HttpModel.Requests;
using ACME.Protocol.Server.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Controllers
{
    [ApiController]
    [AddNextNonce]
    public class OrderController : ControllerBase
    {
        [Route("/new-order", Name = "NewOrder")]
        [HttpPost]
        public async Task<ActionResult<HttpModel.Order>> CreateOrder(AcmeHttpRequest<CreateOrder> request)
        {
            return Ok();
        }
    }
}
