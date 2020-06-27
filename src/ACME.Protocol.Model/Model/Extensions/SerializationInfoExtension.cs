using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace TGIT.ACME.Protocol.Model.Extensions
{
    public static class SerializationInfoExtension
    {
        public static T GetValue<T>(this SerializationInfo info, string name)
        {
            if (info is null)
                throw new ArgumentNullException(nameof(info));

            return (T)info.GetValue(name, typeof(T));
        }

        [return: MaybeNull]
        public static T TryGetValue<T>(this SerializationInfo info, string name)
        {
            if (info is null)
                throw new ArgumentNullException(nameof(info));

            try
            {
                return (T)info.GetValue(name, typeof(T));
            }
            catch (SerializationException)
            {
                return default;
            }
        }
    }
}
