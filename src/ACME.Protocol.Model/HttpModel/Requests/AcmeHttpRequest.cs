using ACME.Protocol.HttpModel.Converters;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ACME.Protocol.HttpModel.Requests
{
    [JsonConverter(typeof(AcmeJsonConverterFactory))]
    public class AcmeHttpRequest
    {
        public AcmeHttpRequest(Base64EncodedRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Header))
                throw new ArgumentException("message", nameof(request.Header));

            if (string.IsNullOrWhiteSpace(request.Signature))
                throw new ArgumentException("message", nameof(request.Signature));

            EncodedHeader = request.Header;
            Signature = request.Signature;

            var headerJson = Base64UrlConverter.ToUtf8String(EncodedHeader);
            Header = JsonSerializer.Deserialize<AcmeRequestHeader>(headerJson);
        }


        public AcmeRequestHeader Header { get; private set; }
        public string EncodedHeader { get; private set; }

        public string Signature { get; private set; }
    }

    [JsonConverter(typeof(AcmeJsonConverterFactory))]
    public class AcmeHttpRequest<TPayload> : AcmeHttpRequest
        where TPayload : class
    {
        public AcmeHttpRequest(Base64EncodedRequest request)
            : base(request)
        {
            EncodedPayload = request.Payload;

            if (string.IsNullOrWhiteSpace(EncodedPayload))
                return;

            var payloadJson = Base64UrlConverter.ToUtf8String(EncodedPayload);
            Payload = JsonSerializer.Deserialize<TPayload>(payloadJson);
        }

        public TPayload? Payload { get; private set; }
        public string? EncodedPayload { get; private set; }
    }
}
