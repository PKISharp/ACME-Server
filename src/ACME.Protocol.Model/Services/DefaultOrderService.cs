using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;
using TGIT.ACME.Protocol.Model.Exceptions;
using TGIT.ACME.Protocol.Storage;

namespace TGIT.ACME.Protocol.Services
{
    public class DefaultOrderService : IOrderService
    {
        private readonly IOrderStore _orderStore;
        private readonly IAuthorizationFactory _authorizationFactory;

        public DefaultOrderService(IOrderStore orderStore, IAuthorizationFactory authorizationFactory)
        {
            _orderStore = orderStore;
            _authorizationFactory = authorizationFactory;
        }

        public async Task<Order> CreateOrderAsync(Account account,
            IEnumerable<Identifier> identifiers, 
            DateTimeOffset? notBefore, DateTimeOffset? notAfter, 
            CancellationToken cancellationToken)
        {
            if (account is null)
                throw new ArgumentNullException(nameof(account));

            var order = new Order(account, identifiers)
            {
                NotBefore = notBefore,
                NotAfter = notAfter
            };

            _authorizationFactory.CreateAuthorizations(order);

            await _orderStore.SaveOrderAsync(order, cancellationToken);

            return order;
        }

        public async Task<byte[]> GetCertificate(Account account, string orderId, CancellationToken cancellationToken)
        {
            if (account is null)
                throw new ArgumentNullException(nameof(account));

            var order = await _orderStore.LoadOrderAsync(orderId, cancellationToken);
            if (order == null)
                throw new NotFoundException();

            ValidateOrderAccess(order, account);
            if (order.Status != OrderStatus.Valid)
                throw new ConflictRequestException(OrderStatus.Valid, order.Status);


            throw new NotImplementedException();
        }

        public async Task<Order?> GetOrderAsync(Account account, string orderId, CancellationToken cancellationToken)
        {
            var order = await _orderStore.LoadOrderAsync(orderId, cancellationToken);
            return order;
        }

        public async Task<Challenge> ProcessChallengeAsync(Account account, string orderId, string authId, string challengeId, CancellationToken cancellationToken)
        {
            var order = await _orderStore.LoadOrderAsync(orderId, cancellationToken);
            var authZ = order?.GetAuthorization(authId);
            var challenge = authZ?.GetChallenge(challengeId);
            
            if (order == null || authZ == null || challenge == null)
                throw new NotFoundException();

            if (order.Status != OrderStatus.Pending)
                throw new ConflictRequestException(OrderStatus.Pending, order.Status);
            if (authZ.Status != AuthorizationStatus.Pending)
                throw new ConflictRequestException(AuthorizationStatus.Pending, authZ.Status);
            if (challenge.Status != ChallengeStatus.Pending)
                throw new ConflictRequestException(ChallengeStatus.Pending, challenge.Status);

            challenge.SetStatus(ChallengeStatus.Processing);
            authZ.SelectChallenge(challenge);

            await _orderStore.SaveOrderAsync(order, cancellationToken);

            return challenge;
        }

        public async Task<Order> ProcessCsr(Account account, string orderId, string csr, CancellationToken cancellationToken)
        {
            var order = await _orderStore.LoadOrderAsync(orderId, cancellationToken);
            throw new NotImplementedException();
        }

        private void ValidateOrderAccess(Order order, Account account)
        {
            if (order.AccountId != account.AccountId)
                throw new NotAllowedException();

            if (account.Status != AccountStatus.Valid)
                throw new NotAllowedException();
        }
    }
}
