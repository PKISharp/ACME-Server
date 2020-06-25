using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.HttpModel.Requests;

namespace TGIT.ACME.Protocol.Services.RequestServices
{
    public interface IRequestValidationService
    {
        Task ValidateRequestAsync(AcmeRawPostRequest request, AcmeHeader header, CancellationToken cancellationToken);
    }
}
