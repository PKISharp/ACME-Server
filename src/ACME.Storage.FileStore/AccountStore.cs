using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;
using TGIT.ACME.Protocol.Model.Exceptions;
using TGIT.ACME.Protocol.Storage;
using TGIT.ACME.Storage.FileStore.Configuration;

namespace TGIT.ACME.Storage.FileStore
{
    public class AccountStore : StoreBase, IAccountStore
    {
        public AccountStore(IOptions<FileStoreOptions> options)
            : base(options)
        { }

        public async Task<Account?> LoadAccountAsync(string accountId, CancellationToken cancellationToken)
        {
            if (!IdentifierRegex.IsMatch(accountId))
                throw new MalformedRequestException("AccountId does not match expected format.");

            var accountPath = Path.Combine(Options.Value.AccountPath,
                accountId, "account.json");

            if (!File.Exists(accountPath))
                return null;

            using (var fileStream = File.OpenRead(accountPath))
            {
                var utf8Bytes = new byte[fileStream.Length];
                await fileStream.ReadAsync(utf8Bytes, cancellationToken);
                var result = JsonConvert.DeserializeObject<Account>(Encoding.UTF8.GetString(utf8Bytes));

                return result;
            }
        }

        public async Task SaveAccountAsync(Account setAccount, CancellationToken cancellationToken)
        {
            if (setAccount is null)
                throw new ArgumentNullException(nameof(setAccount));

            var accountPath = Path.Combine(Options.Value.AccountPath,
                setAccount.AccountId, "account.json");

            Directory.CreateDirectory(Path.GetDirectoryName(accountPath));

            using (var fileStream = File.Open(accountPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                var existingAccount = await LoadAccountAsync(setAccount.AccountId, cancellationToken);
                if (existingAccount != null && existingAccount.Version != setAccount.Version)
                    throw new ConcurrencyException();

                setAccount.Version = DateTime.UtcNow.Ticks;
                var utf8Bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(setAccount));
                await fileStream.WriteAsync(utf8Bytes, cancellationToken);
            }
        }
    }
}
