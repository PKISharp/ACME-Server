using ACME.Protocol.HttpModel.Converters;
using System;
using System.Text.Json.Serialization;

namespace ACME.Protocol.HttpModel.Requests
{
    [JsonConverter(typeof(AcmeJsonConverterFactory))]
    public class AcmeHttpRequest
    {
        public AcmeHttpRequest(Base64EncodedRequest request, AcmeRequestHeader header)
        {
            if (string.IsNullOrWhiteSpace(request.Header))
                throw new ArgumentException("message", nameof(request.Header));

            if (string.IsNullOrWhiteSpace(request.Signature))
                throw new ArgumentException("message", nameof(request.Signature));

            EncodedHeader = request.Header;
            Signature = request.Signature;

            Header = header;
        }


        public AcmeRequestHeader Header { get; private set; }
        public string EncodedHeader { get; private set; }

        public string? EncodedPayload { get; protected set; }

        public string Signature { get; private set; }
    }

    [JsonConverter(typeof(AcmeJsonConverterFactory))]
    public class AcmeHttpRequest<TPayload> : AcmeHttpRequest
        where TPayload : class
    {
        public AcmeHttpRequest(Base64EncodedRequest request, AcmeRequestHeader header, TPayload? payload)
            : base(request, header)
        {
            EncodedPayload = request.Payload;
            Payload = payload;
        }

        public TPayload? Payload { get; private set; }
    }
}
