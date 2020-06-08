using System;
using System.Collections.Generic;
using System.Linq;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Order
    {
        private static Dictionary<OrderStatus, OrderStatus[]> _validStatusTransitions =
            new Dictionary<OrderStatus, OrderStatus[]>
            {
                { OrderStatus.Pending, new [] { OrderStatus.Ready, OrderStatus.Invalid } },
                { OrderStatus.Ready, new [] { OrderStatus.Processing, OrderStatus.Invalid } },
                { OrderStatus.Processing, new [] { OrderStatus.Valid, OrderStatus.Invalid } },
            };

        private string? _orderId;
        private string? _accountId;

        internal Order()
        {
            Identifiers = new List<Identifier>();
            Authorizations = new List<Authorization>();
        }

        internal Order(Account account, IEnumerable<Identifier> identifiers, IEnumerable<Authorization> authorizations)
        {
            _orderId = GuidString.NewValue();
            Status = OrderStatus.Pending;

            AccountId = account.AccountId;

            Identifiers = new List<Identifier>(identifiers);
            Authorizations = new List<Authorization>(authorizations);
        }

        public string OrderId
        {
            get => _orderId ?? throw new NotInitializedException();
            set => _orderId = value;
        }

        public string AccountId {
            get => _accountId ?? throw new NotInitializedException(); 
            set => _accountId = value; 
        }


        public OrderStatus Status { get; private set; }
        public List<Identifier> Identifiers { get; set; }

        public List<Authorization> Authorizations { get; set; }
        public DateTimeOffset? NotBefore { get; internal set; }
        public DateTimeOffset? NotAfter { get; internal set; }
        public HttpModel.AcmeError? Error { get; internal set; }
        public DateTimeOffset? Expires { get; internal set; }

        /// <summary>
        /// Concurrency Token
        /// </summary>
        public long Version { get; set; }

        public Authorization? GetAuthorization(string authId) 
            => Authorizations.FirstOrDefault(x => x.AuthorizationId == authId);


        internal void SetStatus(OrderStatus nextStatus)
        {
            if (!_validStatusTransitions.ContainsKey(Status))
                throw new InvalidOperationException($"Cannot do challenge status transition from '{Status}'.");

            if (!_validStatusTransitions[Status].Contains(nextStatus))
                throw new InvalidOperationException($"Cannot do challenge status transition from '{Status}' to {nextStatus}.");

            Status = nextStatus;
        }

        internal void SetStatusFromAuthorizations()
        {
            if (Authorizations.All(a => a.Status == AuthorizationStatus.Valid))
                SetStatus(OrderStatus.Ready);

            if (Authorizations.Any(a => a.Status.IsInvalid()))
                SetStatus(OrderStatus.Invalid);
        }
    }
}
