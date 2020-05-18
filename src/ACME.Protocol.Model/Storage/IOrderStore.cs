using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Storage
{
    public interface IOrderStore
    {
        Task SaveOrderAsync(Order order, CancellationToken cancellationToken);
        Task<Order?> LoadOrderAsync(string orderId, Account account, CancellationToken cancellationToken);
    }
}
