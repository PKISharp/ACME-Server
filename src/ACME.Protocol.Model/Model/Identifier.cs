using System;
using System.Diagnostics.CodeAnalysis;

namespace TG_IT.ACME.Protocol.Model
{
    public struct Identifier : IEquatable<Identifier>
    {
        private string? _type;
        private string? _value;

        public string Type { 
            get => _type; 
            set => _type = value?.Trim().ToLowerInvariant();
        }
        public string Value { 
            get => _value; 
            set => _value = value?.Trim().ToLowerInvariant(); 
        }

        public override bool Equals(object? obj)
        {
            return obj is Identifier identifier && Equals(identifier);
        }

        public bool Equals([AllowNull] Identifier other)
        {
            return Type == other.Type &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }

        public static bool operator ==(Identifier left, Identifier right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Identifier left, Identifier right)
        {
            return !(left == right);
        }
    }
}
