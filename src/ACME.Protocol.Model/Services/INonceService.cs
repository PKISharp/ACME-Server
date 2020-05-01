using System.Threading;
using System.Threading.Tasks;
using TG_IT.ACME.Protocol.Model;

namespace TG_IT.ACME.Protocol.Services
{
    public interface INonceService
    {
        Task<Nonce> CreateNonceAsync(CancellationToken cancellationToken);

    }
}
