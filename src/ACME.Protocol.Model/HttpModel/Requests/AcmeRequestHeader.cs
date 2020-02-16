using ACME.Protocol.HttpModel.Converters;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace ACME.Protocol.HttpModel.Requests
{
    public class AcmeRequestHeader
    {
        public string? Alg { get; set; }
        public string? Nonce { get; set; }
        public string? Url { get; set; }

        public string? Kid { get; set; }

        [JsonConverter(typeof(JwkConverter)), JsonPropertyName("jwk")]
        public KeyWrapper? Keys { get; set; }
    }

    public class KeyWrapper
    {
        public JsonWebKey Jwk { get; set; }
        public SecurityKey SecurityKey { get; set; }
    }
}
