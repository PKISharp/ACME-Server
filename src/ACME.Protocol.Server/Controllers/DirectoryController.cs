using Microsoft.AspNetCore.Mvc;

namespace ACME.Protocol.Server.Controllers
{
    public class DirectoryController : ControllerBase
    {
        [Route("/")]
        public ActionResult<HttpModel.Directory> GetDirectory()
        {
            return new HttpModel.Directory
            {
                NewNonce = Url.ActionLink(nameof(NonceController.CreateNewNonce), nameof(NonceController))
            };
        }
    }
}
