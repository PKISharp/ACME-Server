using ACME.Protocol.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ACME.Protocol.Model
{
    public class Order
    {
        private string? _orderId;

        public Order() {
            Identifiers = new List<Identifier>();
            Authorizations = new List<Authorization>();
        }

        public Order(Account account, IEnumerable<Identifier> identifiers, IEnumerable<Authorization> authorizations)
        {
            _orderId = Base64UrlEncoder.Encode(Guid.NewGuid().ToByteArray());
            Status = OrderStatus.Pending;

            Account = account;

            Identifiers = new List<Identifier>(identifiers);
            Authorizations = new List<Authorization>(authorizations);
        }

        public string OrderId { 
            get => _orderId ?? throw new NotInitializedException();
            set => _orderId = value;
        }

        [JsonIgnore]
        public Account Account { get; set; }


        public OrderStatus Status { get; set; }

        public List<Identifier> Identifiers { get; }

        public List<Authorization> Authorizations { get; }
        public DateTimeOffset? NotBefore { get; internal set; }
        public DateTimeOffset? NotAfter { get; internal set; }
    }
}
