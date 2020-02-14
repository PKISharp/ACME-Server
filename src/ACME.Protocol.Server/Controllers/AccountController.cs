using ACME.Protocol.HttpModel.Requests;
using ACME.Protocol.Server.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ACME.Protocol.Server.Controllers
{
    public class AccountController
    {
        [Route("/new-account", Name = "NewAccount")]
        [AddNextNonce]
        public Task<ActionResult> CreateOrGetAccount(CreateOrGetAccount request)
        {
            return null;
        }
    }
}
