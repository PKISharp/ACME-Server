using ACME.Protocol.HttpModel.Converters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;

namespace ACME.Protocol.Extensions
{
    public static class JsonWebKeyExtensions
    {
        public static RSAParameters ToRSAParameters(this JsonWebKey jwk)
        {
            if (jwk.Kty != "RSA")
                throw new InvalidOperationException("Can't create RSA-Paramters from non RSA jwk");

            return new RSAParameters
            {
                D = Base64UrlConverter.ToByteArray(jwk.D),
                DP = Base64UrlConverter.ToByteArray(jwk.DP),
                DQ = Base64UrlConverter.ToByteArray(jwk.DQ),
                Exponent = Base64UrlConverter.ToByteArray(jwk.E),
                InverseQ = Base64UrlConverter.ToByteArray(jwk.QI),
                Modulus = Base64UrlConverter.ToByteArray(jwk.N),
                P = Base64UrlConverter.ToByteArray(jwk.P),
                Q = Base64UrlConverter.ToByteArray(jwk.Q),
            };
        }
    }
}
