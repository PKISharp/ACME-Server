using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TG_IT.ACME.Protocol.Model;
using TG_IT.ACME.Protocol.Model.Exceptions;
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
            if (account is null)
                throw new ArgumentNullException(nameof(account));

            var authorizations = new List<Authorization>();

            var order = new Order(account, identifiers, authorizations)
            {
                NotBefore = notBefore,
                NotAfter = notAfter
            };

            await _orderStore.SaveOrderAsync(order, cancellationToken);

            return order;
        }

        public async Task<byte[]> GetCertificate(Account account, string orderId, CancellationToken cancellationToken)
        {
            var order = await _orderStore.LoadOrderAsync(orderId, account, cancellationToken);
            throw new NotImplementedException();
        }

        public async Task<Order?> GetOrderAsync(Account account, string orderId, CancellationToken cancellationToken)
        {
            var order = await _orderStore.LoadOrderAsync(orderId, account, cancellationToken);
            return order;
        }

        public async Task<Challenge> ProcessChallengeAsync(Account account, string orderId, string authId, string challengeId, CancellationToken cancellationToken)
        {
            var order = await _orderStore.LoadOrderAsync(orderId, account, cancellationToken);
            var authZ = order?.GetAuthorization(authId);
            var challenge = authZ?.GetChallenge(challengeId);
            
            if (challenge == null)
                throw new MalformedRequestException("Could not locate challenge");
            if (order!.Status != OrderStatus.Pending)

            throw new NotImplementedException();
        }

        public async Task<Order> ProcessCsr(Account account, string orderId, string csr, CancellationToken cancellationToken)
        {
            var order = await _orderStore.LoadOrderAsync(orderId, account, cancellationToken);
            throw new NotImplementedException();
        }
    }
}
