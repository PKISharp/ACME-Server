using ACME.Protocol.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace ACME.Protocol.Model
{
    public class AcmeAccount
    {
        public AcmeAccount()
        {
            AccountId = Base64UrlEncoder.Encode(Guid.NewGuid().ToByteArray());
        }

        public string AccountId { get; internal set; }

        public AccountStatus Status { get; set; }

        public Jwk Jwk { get; set; }

        public List<string>? Contact { get; set; }

        public DateTimeOffset? TOSAccepted { get; set; }
    }
}
