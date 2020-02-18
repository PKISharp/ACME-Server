using ACME.Protocol.HttpModel.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public interface IAcmeSignatureService
    {
        Task ValidateAcmeRequestAsync(AcmeHttpRequest request, CancellationToken cancellationToken);
    }
}
