using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace TG_IT.ACME.Protocol.Model
{
    public class Account
    {
        public Account()
        {
            AccountId = new GuidString();
        }

        public string AccountId { get; internal set; }

        public AccountStatus Status { get; set; }

        public Jwk Jwk { get; set; }

        public List<string>? Contact { get; set; }

        public DateTimeOffset? TOSAccepted { get; set; }
    }
}
