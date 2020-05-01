using Microsoft.IdentityModel.Tokens;

namespace TG_IT.ACME.Protocol.Model
{
    public class Jwk
    {
        private JsonWebKey? _jsonWebKey;
        private string? _jsonKeyHash;

        private Jwk() { }

        public Jwk(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new System.ArgumentNullException(nameof(json));

            Json = json;
        }

        public string Json { get; set; }


        public JsonWebKey GetJwkSecurityKey()
            => _jsonWebKey ??= JsonWebKey.Create(Json);
    }
}
