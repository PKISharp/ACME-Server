using ACME.Server.Model;
using System.Threading;
using System.Threading.Tasks;

namespace TG_IT.ACME.Protocol.Storage
{
    public interface IOrderStore
    {
        Task SaveOrderAsync(Order order, CancellationToken cancellationToken);
        Task<Order> LoadOrderAsync(string orderId, Account account, CancellationToken cancellationToken);
    }
}
