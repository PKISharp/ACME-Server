using ACME.Protocol.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Storage
{
    public interface IOrderStore
    {
        Task SaveOrderAsync(Order order, CancellationToken cancellationToken);
        Task<Order> LoadOrderAsync(string orderId, Account account, CancellationToken cancellationToken);
    }
}
