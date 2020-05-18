using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.HttpModel.Requests;
using TGIT.ACME.Protocol.Model.Exceptions;
using TGIT.ACME.Protocol.Services;
using TGIT.ACME.Server.Filters;

namespace TGIT.ACME.Server.Controllers
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
        public async Task<ActionResult<Protocol.HttpModel.Account>> CreateOrGetAccount(AcmeHttpRequest<CreateOrGetAccount> request)
        {
            if(request.Payload!.OnlyReturnExisting)
                return await FindAccountAsync(request);

            return await CreateAccountAsync(request);
        }

        private async Task<ActionResult<Protocol.HttpModel.Account>> CreateAccountAsync(AcmeHttpRequest<CreateOrGetAccount> request)
        {
            if (request.Payload == null)
                throw new MalformedRequestException("Payload was empty or could not be read.");

            var account = await _accountService.CreateAccountAsync(
                request.Header.Jwk!, //Post requests are validated, JWK exists.
                request.Payload.Contact,
                request.Payload.TermsOfServiceAgreed,
                HttpContext.RequestAborted);

            var ordersUrl = Url.RouteUrl("OrderList", new { accountId = account.AccountId }, "https");
            var accountResponse = new Protocol.HttpModel.Account(account, ordersUrl);

            var accountUrl = Url.RouteUrl("Account", new { accountId = account.AccountId }, "https");
            return new CreatedResult(accountUrl, accountResponse);
        }

        private Task<ActionResult<Protocol.HttpModel.Account>> FindAccountAsync(AcmeHttpRequest<CreateOrGetAccount> request)
        {
            throw new NotImplementedException();
        }

        [Route("/account/{accountId}", Name = "Account")]
        [HttpPost, HttpPut]
        public Task<ActionResult<Protocol.HttpModel.Account>> SetAccount(string accountId)
        {
            throw new NotImplementedException();
        }

        [Route("/account/{accountId}/orders", Name = "OrderList")]
        [HttpPost]
        public Task<ActionResult<Protocol.HttpModel.OrdersList>> GetOrdersList(string accountId, AcmeHttpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
