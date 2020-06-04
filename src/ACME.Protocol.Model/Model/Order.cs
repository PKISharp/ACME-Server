using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using TGIT.ACME.Protocol.HttpModel;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Order
    {
        private string? _orderId;
        private Account? _account;
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
            Account = account;

            Identifiers = new List<Identifier>(identifiers);
            Authorizations = new List<Authorization>(authorizations);
        }

        public string OrderId
        {
            get => _orderId ?? throw new NotInitializedException();
            set => _orderId = value;
        }

        [JsonIgnore]
        public Account Account
        {
            get => _account ?? throw new NotInitializedException();
            set => _account = value;
        }
        public string AccountId {
            get => _accountId ?? throw new NotInitializedException(); 
            set => _accountId = value; 
        }


        public OrderStatus Status { get; set; }
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
    }
}
