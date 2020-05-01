using ACME.Server.HttpModel.Requests;
using TG_IT.ACME.Server.Filters;
using ACME.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TG_IT.ACME.Server.Controllers
{
    [ApiController]
    [AddNextNonce]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("/new-account", Name = "NewAccount")]
        [HttpPost]
        public async Task<ActionResult<HttpModel.Account>> CreateOrGetAccount(AcmeHttpRequest<CreateOrGetAccount> request)
        {
            if(request.Payload!.OnlyReturnExisting)
            {
                return await FindAccountAsync(request);
            }

            return await CreateAccountAsync(request);
        }

        private async Task<ActionResult<HttpModel.Account>> CreateAccountAsync(AcmeHttpRequest<CreateOrGetAccount> request)
        {
            var account = await _accountService.CreateAccountAsync(
                request.Header!.Jwk,
                request.Payload!.Contact,
                request.Payload!.TermsOfServiceAgreed,
                HttpContext.RequestAborted);

            var accountResponse = new HttpModel.Account
            {
                Status = account.Status.ToString(),

                Contact = account.Contact,
                TermsOfServiceAgreed = account.TOSAccepted.HasValue,

                ExternalAccountBinding = null,
                Orders = Url.RouteUrl("OrderList", new { accountId = account.AccountId }, "https")
            };

            var accountUrl = Url.RouteUrl("Account", new { accountId = account.AccountId }, "https");
            return new CreatedResult(accountUrl, accountResponse);
        }

        private Task<ActionResult<HttpModel.Account>> FindAccountAsync(AcmeHttpRequest<CreateOrGetAccount> request)
        {
            throw new NotImplementedException();
        }

        [Route("/account/{accountId}", Name = "Account")]
        [HttpPost, HttpPut]
        public async Task<ActionResult<HttpModel.Account>> SetAccount(string accountId)
        {
            return Ok();
        }

        [Route("/account/{accountId}/orders", Name = "OrderList")]
        [HttpPost]
        public async Task<ActionResult<HttpModel.OrdersList>> GetOrdersList(string accountId, AcmeHttpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
