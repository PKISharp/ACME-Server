using Microsoft.IdentityModel.Tokens;
using System;

namespace TGIT.ACME.Protocol.Model
{
    internal class GuidString
    {
        public GuidString()
        {
            Value = Base64UrlEncoder.Encode(Guid.NewGuid().ToByteArray());
        }

        public string Value { get; }

        public static string NewValue() => new GuidString().Value;
    }
}
