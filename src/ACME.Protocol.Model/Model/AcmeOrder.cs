using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Protocol.Model
{
    public class AcmeOrder
    {
        public AcmeOrder()
        {
            OrderId = Base64UrlEncoder.Encode(Guid.NewGuid().ToByteArray());
            Status = OrderStatus.Pending;
        }

        public string OrderId { get; set; }

        public OrderStatus Status { get; set; }

        public AcmeChallange[] Challanges { get; set; }
    }

    public class AcmeChallange
    {

    }
}
