using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    [Serializable]
    public class Order : IVersioned, ISerializable
    {
        private static readonly Dictionary<OrderStatus, OrderStatus[]> _validStatusTransitions =
            new Dictionary<OrderStatus, OrderStatus[]>
            {
                { OrderStatus.Pending, new [] { OrderStatus.Ready, OrderStatus.Invalid } },
                { OrderStatus.Ready, new [] { OrderStatus.Processing, OrderStatus.Invalid } },
                { OrderStatus.Processing, new [] { OrderStatus.Valid, OrderStatus.Invalid } },
            };

        private string? _orderId;
        private string? _accountId;

        internal Order(Account account, IEnumerable<Identifier> identifiers)
        {
            _orderId = GuidString.NewValue();
            Status = OrderStatus.Pending;

            AccountId = account.AccountId;

            Identifiers = new List<Identifier>(identifiers);
            Authorizations = new List<Authorization>();
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


        public OrderStatus Status { get; set; }
        public List<Identifier> Identifiers { get; private set; }

        public List<Authorization> Authorizations { get; private set; }
        public DateTimeOffset? NotBefore { get; set; }
        public DateTimeOffset? NotAfter { get; set; }
        public HttpModel.AcmeError? Error { get; set; }
        public DateTimeOffset? Expires { get; set; }

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



        protected Order(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
