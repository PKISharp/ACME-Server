using ACME.Protocol.HttpModel.Requests;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ACME.Protocol.HttpModel.Converters
{
    public class AcmeJsonConverter : JsonConverter<AcmeHttpRequest>
    {
        public override AcmeHttpRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var encodedRequest = ReadEncodedRequest(ref reader, options);

            var headerJson = Base64UrlEncoder.Decode(encodedRequest.Header);
            var header = JsonSerializer.Deserialize<AcmeRequestHeader>(headerJson, options);

            var result = new AcmeHttpRequest(encodedRequest, header);

            return result;
        }

        protected Base64EncodedRequest ReadEncodedRequest(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<Base64EncodedRequest>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, AcmeHttpRequest value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    public class AcmeJsonConverter<TPayload> : AcmeJsonConverter
        where TPayload : class
    {
        public override AcmeHttpRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var encodedRequest = ReadEncodedRequest(ref reader, options);

            var headerJson = Base64UrlEncoder.Decode(encodedRequest.Header);
            var header = JsonSerializer.Deserialize<AcmeRequestHeader>(headerJson, options);

            if (string.IsNullOrWhiteSpace(encodedRequest.Payload))
                return new AcmeHttpRequest<TPayload>(encodedRequest, header, null);

            var payloadJson = Base64UrlEncoder.Decode(encodedRequest.Payload);
            var payload = JsonSerializer.Deserialize<TPayload>(payloadJson);

            return new AcmeHttpRequest<TPayload>(encodedRequest, header, payload);
        }

        public override void Write(Utf8JsonWriter writer, AcmeHttpRequest value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
