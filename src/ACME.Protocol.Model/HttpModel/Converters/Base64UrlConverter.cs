using System;
using System.Text;

namespace ACME.Protocol.HttpModel.Converters
{
    public static class Base64UrlConverter
    {
        public static byte[]? ToByteArray(string base64UrlEncodedString)
        {
            if (string.IsNullOrWhiteSpace(base64UrlEncodedString))
                return null;

            var base64String = base64UrlEncodedString.Replace("-", "+").Replace("_", "/");
            var paddedBase64 = base64String.PadRight((int)Math.Ceiling(base64String.Length / 4.0) * 4, '=');
            return Convert.FromBase64String(paddedBase64);
        }

        public static string? ToUtf8String(string base64UrlEncodedString)
        {
            if (string.IsNullOrWhiteSpace(base64UrlEncodedString))
                return null;

            var base64Bytes = ToByteArray(base64UrlEncodedString);
            var utf8String = Encoding.UTF8.GetString(base64Bytes!);

            return utf8String;
        }
    }
}
