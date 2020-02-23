using ACME.Protocol.Model;
using ACME.Protocol.Storage;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Account account,
            IEnumerable<Identifier> identifiers, 
            DateTimeOffset? notBefore, DateTimeOffset? notAfter, 
            CancellationToken cancellationToken);
    }

    public class DefaultOrderService : IOrderService
    {
        private readonly IOrderStore _orderStore;

        public DefaultOrderService(IOrderStore orderStore)
        {
            _orderStore = orderStore;
        }

        public async Task<Order> CreateOrderAsync(Account account,
            IEnumerable<Identifier> identifiers, 
            DateTimeOffset? notBefore, DateTimeOffset? notAfter, 
            CancellationToken cancellationToken)
        {
            var authorizations = new List<Authorization>();

            var order = new Order(account, identifiers, authorizations)
            {
                NotBefore = notBefore,
                NotAfter = notAfter
            };

            await _orderStore.SaveOrderAsync(order, cancellationToken);

            return order;
        }
    }
}
