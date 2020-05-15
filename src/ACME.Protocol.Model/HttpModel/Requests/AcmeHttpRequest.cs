using System;
using System.Text.Json.Serialization;
using TG_IT.ACME.Protocol.HttpModel.Converters;

namespace TG_IT.ACME.Protocol.HttpModel.Requests
{
    [JsonConverter(typeof(AcmeJsonConverterFactory))]
    public class AcmeHttpRequest
    {
        public AcmeHttpRequest(Base64EncodedRequest request, AcmeRequestHeader header)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Header))
                throw new ArgumentException("Request body header is missing", nameof(request));

            if (string.IsNullOrWhiteSpace(request.Signature))
                throw new ArgumentException("Request body signature is missing", nameof(request));

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
