using Microsoft.IdentityModel.Tokens;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TGIT.ACME.Protocol.HttpModel.Requests;
using TGIT.ACME.Protocol.Infrastructure;

namespace TGIT.ACME.Protocol.HttpModel.Converters
{
    public class AcmeJsonConverter : JsonConverter<AcmePostRequest>
    {
        public override AcmePostRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var encodedRequest = ReadEncodedRequest(ref reader, options);
            var header = ReadHeader(encodedRequest, options);

            var result = new AcmePostRequest(header, encodedRequest.Signature);

            return result;
        }

        protected static DecodedHeader ReadHeader(AcmeRawPostRequest encodedRequest, JsonSerializerOptions options)
        {
            if (encodedRequest is null)
                throw new ArgumentNullException(nameof(encodedRequest));

            var headerJson = Base64UrlEncoder.Decode(encodedRequest.Header);
            var headerValue = JsonSerializer.Deserialize<AcmeHeader>(headerJson, options);
            var header = new DecodedHeader(encodedRequest.Header, headerValue);

            return header;
        }

        protected static AcmeRawPostRequest ReadEncodedRequest(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<AcmeRawPostRequest>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, AcmePostRequest value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException();
        }
    }

    public class AcmeJsonConverter<TPayload> : AcmeJsonConverter
        where TPayload : class?
    {
        public override AcmePostRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var encodedRequest = ReadEncodedRequest(ref reader, options);

            var header = ReadHeader(encodedRequest, options);

            if (string.IsNullOrWhiteSpace(encodedRequest.Payload))
            {
                var payload = new DecodedPayload<TPayload>("", null!);
                return new AcmePostRequest<TPayload>(header, payload, encodedRequest.Signature);
            }
            else
            {
                var payloadJson = Base64UrlEncoder.Decode(encodedRequest.Payload);
                var payloadValue = JsonSerializer.Deserialize<TPayload>(payloadJson, options);
                var payload = new DecodedPayload<TPayload>(payloadJson, payloadValue);

                return new AcmePostRequest<TPayload>(header, payload, encodedRequest.Signature);
            }
        }

        public override void Write(Utf8JsonWriter writer, AcmePostRequest value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException();
        }
    }
}
