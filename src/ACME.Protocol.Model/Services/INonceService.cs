using ACME.Protocol.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public interface INonceService
    {
        Task<AcmeNonce> CreateNonceAsync(AcmeRequestContext context, CancellationToken cancellationToken);

        Task ValidateNonceAsync(AcmeRequestContext context, CancellationToken cancellationToken);
    }
}
