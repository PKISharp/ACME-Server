using ACME.Protocol.HttpModel.Requests;
using ACME.Protocol.Server.Filters;
using ACME.Protocol.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Controllers
{
    [ApiController]
    [AddNextNonce]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;

        public OrderController(IOrderService orderService, IAccountService accountService)
        {
            _orderService = orderService;
            _accountService = accountService;
        }

        [Route("/new-order", Name = "NewOrder")]
        [HttpPost]
        public async Task<ActionResult<HttpModel.Order>> CreateOrder(AcmeHttpRequest<CreateOrder> request)
        {
            var account = await _accountService.FromRequestAsync(request, HttpContext.RequestAborted);

            var orderRequest = request.Payload!;

            var identifiers = orderRequest.Identifiers.Select(x =>
                new Model.Identifier
                {
                    Type = x.Type,
                    Value = x.Value
                });

            var order = await _orderService.CreateOrderAsync(
                account, identifiers,
                orderRequest.NotBefore, orderRequest.NotAfter,
                HttpContext.RequestAborted);

            var orderResponse = new HttpModel.Order
            {
                Status = order.Status.ToString().ToLowerInvariant(),
                

                Identifiers = order.Identifiers
                    .Select(x => new HttpModel.Identifier { Type = x.Type, Value = x.Value })
                    .ToList(),
                Authorizations = order.Authorizations
                    .Select(x => Url.RouteUrl("GetAuthorization", new { orderId = order.OrderId, authId = x.AuthorizationId }, "https"))
                    .ToList(),

                Finalize = Url.RouteUrl("FinalizeOrder", new { orderId = order.OrderId }, "https")
                //TODO: Copy all neccessary data
            };

            var orderUrl = Url.RouteUrl("GetOrder", new { orderId = order.OrderId }, "https");
            return new CreatedResult(orderUrl, orderResponse);
        }

        [Route("/order/{orderId}", Name = "GetOrder")]
        [HttpPost]
        public async Task<ActionResult<HttpModel.Order>> GetOrder(string orderId, AcmeHttpRequest request)
        {
            throw new NotImplementedException();
        }

        [Route("/order/{orderId}/auth/{authId}", Name = "GetAuthorization")]
        [HttpPost]
        public async Task<ActionResult<HttpModel.Order>> GetAuthorizations(string orderId, string authId, AcmeHttpRequest request)
        {
            throw new NotImplementedException();

        }

        [Route("/order/{orderId}/finalize", Name = "FinalizeOrder")]
        [HttpPost]
        public async Task<ActionResult<HttpModel.Order>> FinalizeOrder(string orderId, AcmeHttpRequest<object> request)
        {
            throw new NotImplementedException();

        }

        [Route("/order/{orderId}/certificate", Name = "GetCertificate")]
        [HttpPost]
        public async Task<ActionResult<byte[]>> GetCertficate(string orderId, AcmeHttpRequest<object> request)
        {
            throw new NotImplementedException();

        }
    }
}
