using System.Threading;
using System.Threading.Tasks;
using TG_IT.ACME.Protocol.HttpModel.Requests;

namespace TG_IT.ACME.Protocol.Services
{
    public interface IRequestValidationService
    {
        Task ValidateRequestHeaderAsync(AcmeHttpRequest request, CancellationToken cancellationToken);
        Task ValidateSignatureAsync(AcmeHttpRequest request, CancellationToken cancellationToken);

        Task ValidateNonceAsync(string? nonce, CancellationToken cancellationToken);

    }
}
