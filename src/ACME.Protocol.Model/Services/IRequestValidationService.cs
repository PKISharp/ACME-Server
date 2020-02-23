using ACME.Protocol.HttpModel.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public interface IRequestValidationService
    {
        Task ValidateRequestHeaderAsync(AcmeHttpRequest request, CancellationToken cancellationToken);
        Task ValidateSignatureAsync(AcmeHttpRequest request, CancellationToken cancellationToken);

        Task ValidateNonceAsync(string? nonce, CancellationToken cancellationToken);

    }
}
