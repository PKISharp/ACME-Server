using ACME.Server.Model;
using System.Threading;
using System.Threading.Tasks;

namespace TG_IT.ACME.Protocol.Services
{
    public interface INonceService
    {
        Task<Nonce> CreateNonceAsync(CancellationToken cancellationToken);

    }
}
