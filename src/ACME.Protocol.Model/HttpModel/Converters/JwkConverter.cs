using ACME.Protocol.Extensions;
using ACME.Protocol.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ACME.Protocol.HttpModel.Converters
{
    public class JwkConverter : JsonConverter<KeyWrapper>
    {
        public override KeyWrapper Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jwkJson = JsonSerializer.Deserialize<object>(ref reader).ToString();
            var jwk = JsonWebKey.Create(jwkJson);

            var securityKey = jwk.Kty switch
            {
                "RSA" => CreateRSAKey(jwk),
                "EC" => CreateECKey(jwk),
                _ => throw new InvalidOperationException("Not supported")
            };

            return new KeyWrapper
            {
                Jwk = jwk,
                SecurityKey = securityKey
            };
        }

        private SecurityKey CreateRSAKey(JsonWebKey jwk)
        {
            var rsaParameters = jwk.ToRSAParameters();
            return new RsaSecurityKey(rsaParameters);
        }

        private SecurityKey CreateECKey(JsonWebKey jwk)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, KeyWrapper value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
