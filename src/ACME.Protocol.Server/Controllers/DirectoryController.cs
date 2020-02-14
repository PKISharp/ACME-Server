using Microsoft.AspNetCore.Mvc;

namespace ACME.Protocol.Server.Controllers
{
    public class DirectoryController : ControllerBase
    {
        [Route("/", Name = "Directory")]
        public ActionResult<HttpModel.Directory> GetDirectory()
        {
            return new HttpModel.Directory
            {
                NewNonce = Url.RouteUrl("NewNonce", null, "https"),
                NewAccount = Url.RouteUrl("NewAccount", null, "https")
            };
        }
    }
}
