using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace TG_IT.ACME.Protocol.Model
{
    internal class GuidString
    {
        public GuidString()
        {
            Value = Base64UrlEncoder.Encode(Guid.NewGuid().ToByteArray());
        }

        public string Value { get; }

        public static implicit operator string(GuidString instance)
        {
            return instance.Value;
        }
    }
}
