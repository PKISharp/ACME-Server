using ACME.Protocol.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Storage
{
    public interface IOrderStore
    {
        Task SaveOrderAsync(AcmeOrder order, CancellationToken cancellationToken);
        Task<AcmeOrder> LoadOrderAsync(string orderId, CancellationToken cancellationToken);
    }
}
