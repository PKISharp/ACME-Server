using ACME.Protocol.Model;
using ACME.Protocol.Storage;
using ACME.Protocol.Store.Filebased.Configuration;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Store.Filebased
{
    public class AccountStore : IAccountStore
    {
        private readonly IOptions<FileStoreOptions> _options;

        public AccountStore(IOptions<FileStoreOptions> options)
        {
            _options = options;
        }

        public Task SaveAccountAsync(AcmeAccount newAccount, CancellationToken cancellationToken)
        {
            var accountPath = System.IO.Path.Combine(_options.Value.AccountPath, newAccount.AccountId.ToString());
            return Task.CompletedTask;
        }
    }
}
