using ACME.Protocol.Model;
using ACME.Protocol.Storage;
using ACME.Protocol.Store.Filebased.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Store.Filebased
{
    public class AccountStore : IAccountStore
    {
        private readonly IOptions<FileStoreOptions> _options;

        private readonly Regex _accountIdRegex;

        public AccountStore(IOptions<FileStoreOptions> options)
        {
            _options = options;
            _accountIdRegex = new Regex("[\\w\\d_-]+", RegexOptions.Compiled);
        }

        public async Task<AcmeAccount> LoadAccountAsync(string accountId, CancellationToken cancellationToken)
        {
            if (!_accountIdRegex.IsMatch(accountId))
                throw new InvalidOperationException("AccountId seems invalid!");

            var accountPath = System.IO.Path.Combine(_options.Value.AccountPath,
                accountId, "account.json");

            var utf8Bytes = await System.IO.File.ReadAllBytesAsync(accountPath, cancellationToken);
            var result = JsonSerializer.Deserialize<AcmeAccount>(utf8Bytes);

            return result;
        }

        public async Task SaveAccountAsync(AcmeAccount newAccount, CancellationToken cancellationToken)
        {
            var accountPath = System.IO.Path.Combine(_options.Value.AccountPath,
                newAccount.AccountId, "account.json");

            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(accountPath));

            var utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(newAccount);
            await System.IO.File.WriteAllBytesAsync(accountPath, utf8Bytes, cancellationToken);
        }
    }
}
