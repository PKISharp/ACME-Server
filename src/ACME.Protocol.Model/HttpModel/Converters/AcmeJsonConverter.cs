using ACME.Protocol.HttpModel.Requests;
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
            var result = new AcmeHttpRequest(encodedRequest);

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

    public class AcmeJsonConverter<TModel> : AcmeJsonConverter
        where TModel : class
    {
        public override AcmeHttpRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var encodedRequest = ReadEncodedRequest(ref reader, options);
            var result = new AcmeHttpRequest<TModel>(encodedRequest);

            return result;
        }

        public override void Write(Utf8JsonWriter writer, AcmeHttpRequest value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
