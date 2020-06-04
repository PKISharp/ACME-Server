using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.HttpModel.Requests;

namespace TGIT.ACME.Protocol.Services
{
    public interface IRequestValidationService
    {
        Task ValidateRequestHeaderAsync(AcmePostRequest request, CancellationToken cancellationToken);
        Task ValidateSignatureAsync(AcmePostRequest request, CancellationToken cancellationToken);

        Task ValidateNonceAsync(string? nonce, CancellationToken cancellationToken);

    }
}
