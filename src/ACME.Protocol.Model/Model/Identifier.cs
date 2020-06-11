using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Identifier : IEquatable<Identifier>
    {
        private static readonly string[] _supportedTypes = new[] { "dns" };

        private string? _type;
        private string? _value;

        public string Type { 
            get => _type ?? throw new NotInitializedException();
            set
            {
                var normalizedType = value?.Trim().ToLowerInvariant();
                if (!_supportedTypes.Contains(value))
                    throw new MalformedRequestException($"Unsupported identifier type: {normalizedType}");

                _type = normalizedType;
            }
        }

        public string Value { 
            get => _value ?? throw new NotInitializedException();
            set => _value = value?.Trim().ToLowerInvariant();
        }

        public bool IsWildcard
            => Value.StartsWith("*", StringComparison.InvariantCulture);

        public override bool Equals(object? obj)
        {
            return obj is Identifier identifier && Equals(identifier);
        }

        public bool Equals([AllowNull] Identifier other)
        {
            return Type == other?.Type &&
                Value == other?.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }

        public static bool operator ==(Identifier left, Identifier right)
        {
            return left?.Equals(right) ?? false;
        }

        public static bool operator !=(Identifier left, Identifier right)
        {
            return !(left == right);
        }
    }
}
