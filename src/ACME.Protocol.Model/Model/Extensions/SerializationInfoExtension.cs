using System;
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
    }
}
