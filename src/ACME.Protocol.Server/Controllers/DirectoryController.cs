using ACME.Protocol.Server.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ACME.Protocol.Server.Controllers
{
    public class DirectoryController : ControllerBase
    {
        private readonly IOptions<ACMEServerOptions> _options;

        public DirectoryController(IOptions<ACMEServerOptions> options)
        {
            _options = options;
        }

        [Route("/", Name = "Directory")]
        public ActionResult<HttpModel.Directory> GetDirectory()
        {
            var options = _options.Value;

            return new HttpModel.Directory
            {
                NewNonce = Url.RouteUrl("NewNonce", null, "https"),
                NewAccount = Url.RouteUrl("NewAccount", null, "https"),
                NewOrder = Url.RouteUrl("NewOrder", null, "https"),
                NewAuthz = null,
                RevokeCert = null,
                KeyChange = Url.RouteUrl("KeyChange", null, "https"),

                Meta = new HttpModel.DirectoryMetadata
                {
                    ExternalAccountRequired = false,
                    CAAIdentities = null,
                    TermsOfService = options.TOS.RequireAgreement ? options.TOS.Url : null,
                    Website = options.WebsiteUrl
                }
            };
        }
    }
}
