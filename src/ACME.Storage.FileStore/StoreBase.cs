using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using TGIT.ACME.Storage.FileStore.Configuration;

namespace TGIT.ACME.Storage.FileStore
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
