using ACME.Protocol.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public interface INonceService
    {
        Task<Nonce> CreateNonceAsync(CancellationToken cancellationToken);

    }
}
