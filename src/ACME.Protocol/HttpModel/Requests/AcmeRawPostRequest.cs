using System.Text.Json.Serialization;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.HttpModel.Requests
{
    public class AcmeRawPostRequest
    {
        private string? _header;
        private string? _signature;

        private AcmeRawPostRequest() { }

        [JsonPropertyName("protected")]
        public string Header { 
            get => _header ?? throw new NotInitializedException();
            set => _header = value;
        }
        
        public string? Payload { get; set; } 
        
        public string Signature { 
            get => _signature ?? throw new NotInitializedException();
            set => _signature = value;
        }
    }
}
