using ACME.Protocol.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;

namespace ACME.Protocol.Model
{
    public class Jwk
    {
        private JsonWebKey _jsonWebKey;
        private string _jsonKeyHash;

        public Jwk(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new System.ArgumentNullException(nameof(json));

            Json = json;
        }

        public string Json { get; private set; }


        public JsonWebKey GetJwkSecurityKey()
            => _jsonWebKey ??= JsonWebKey.Create(Json);
    }
}
