using ACME.Protocol.HttpModel.Requests;
using ACME.Protocol.Server.Filters;
using ACME.Protocol.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Controllers
{
    [ApiController]
    [ValidateNonce, AddNextNonce]
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
            var account = await _accountService.CreateAccountAsync(
                request.Header!.Keys,
                request.Payload!.Contact,
                request.Payload!.TermsOfServiceAgreed,
                HttpContext.RequestAborted);

            var accountResponse = new HttpModel.Account
            {
                Status = account.Status.ToString(),

                Contact = account.Contact,
                TermsOfServiceAgreed = account.TOSAccepted.HasValue,

                ExternalAccountBinding = null,
                Orders = Url.RouteUrl("Orders", new { AccountId = account.AccountId }, "https")
            };

            var accountUrl = Url.RouteUrl("Account", new { AccountId = account.AccountId }, "https");
            return new CreatedResult(accountUrl, accountResponse);
        }

        [Route("/account", Name = "Account")]
        [HttpPost, HttpPut]
        public async Task<ActionResult<HttpModel.Account>> SetAccount()
        {
            return Ok();
        }
    }
}
