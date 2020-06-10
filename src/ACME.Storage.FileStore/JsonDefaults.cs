using Newtonsoft.Json;

namespace TGIT.ACME.Storage.FileStore
{
    internal static class JsonDefaults
    {
        public static readonly JsonSerializerSettings Settings =
            new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All };
    }
}
