using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TG_IT.ACME.Protocol.Model;
using TG_IT.ACME.Protocol.Storage;

namespace TG_IT.ACME.Protocol.Services
{
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
