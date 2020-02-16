using ACME.Protocol.HttpModel.Converters;
using ACME.Protocol.Model;
using System.Text.Json.Serialization;

namespace ACME.Protocol.HttpModel.Requests
{
    public class AcmeRequestHeader
    {
        public string? Nonce { get; set; }
        public string? Url { get; set; }


        public string? Alg { get; set; }
        public string? Kid { get; set; }

        [JsonConverter(typeof(JwkConverter))]
        public KeyWrapper? Jwk { get; set; }
        public KeyWrapper? Keys => Jwk;
    }
}
