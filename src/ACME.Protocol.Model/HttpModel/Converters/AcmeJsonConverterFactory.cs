using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using TG_IT.ACME.Protocol.HttpModel.Requests;

namespace TG_IT.ACME.Protocol.HttpModel.Converters
{
    public class AcmeJsonConverterFactory : JsonConverterFactory
    {
        private Dictionary<Type, AcmeJsonConverter> _converterCache;

        public AcmeJsonConverterFactory()
        {
            _converterCache = new Dictionary<Type, AcmeJsonConverter>();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(AcmeHttpRequest).IsAssignableFrom(typeToConvert);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert is null)
                throw new ArgumentNullException(nameof(typeToConvert));

            if (typeToConvert.IsGenericType)
            {
                var genericType = typeToConvert.GetGenericArguments()[0];

                if (_converterCache.TryGetValue(genericType, out var converter))
                    return converter;

                var converterType = typeof(AcmeJsonConverter<>).MakeGenericType(genericType);
                converter = (AcmeJsonConverter)Activator.CreateInstance(converterType)!;

                _converterCache.Add(genericType, converter);
                return converter;
            }

            return new AcmeJsonConverter();
        }
    }
}
