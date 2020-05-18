using System.Text.Json.Serialization;

namespace TGIT.ACME.Protocol.HttpModel.Requests
{
    public class Base64EncodedRequest
    {
        [JsonPropertyName("protected")]
        public string? Header { get; set; }
        public string? Payload { get; set; } 
        public string? Signature { get; set; }
    }
}
