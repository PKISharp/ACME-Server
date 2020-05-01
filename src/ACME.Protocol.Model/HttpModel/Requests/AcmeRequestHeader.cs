using System;
using System.Linq;
using System.Text.Json.Serialization;
using TG_IT.ACME.Protocol.HttpModel.Converters;
using TG_IT.ACME.Protocol.Model;

namespace TG_IT.ACME.Protocol.HttpModel.Requests
{
    public class AcmeRequestHeader
    {
        public string? Nonce { get; set; }
        public string? Url { get; set; }


        public string? Alg { get; set; }
        public string? Kid { get; set; }

        [JsonConverter(typeof(JwkConverter))]
        public Jwk? Jwk { get; set; }

        public string GetAccountId()
        {
            if (Kid == null)
                throw new InvalidOperationException();

            return Kid.Split('/').Last();
        }
    }
}
