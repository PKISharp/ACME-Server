using ACME.Protocol.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public interface INonceService
    {
        Task<AcmeNonce> CreateNonceAsync(CancellationToken cancellationToken);

        Task ValidateNonceAsync(string? nonce, CancellationToken cancellationToken);
    }
}
