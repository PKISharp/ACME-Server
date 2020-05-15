using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using TG_IT.ACME.Protocol.Storage.FileStore.Configuration;

namespace TG_IT.ACME.Protocol.Storage.FileStore
{
    public class StoreBase
    {
        protected IOptions<FileStoreOptions> Options { get; }
        protected Regex IdentifierRegex { get; }

        public StoreBase(IOptions<FileStoreOptions> options)
        {
            Options = options;
            IdentifierRegex = new Regex("[\\w\\d_-]+", RegexOptions.Compiled);
        }
    }
}
