using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TG_IT.ACME.Protocol.Model;
using TG_IT.ACME.Protocol.Storage.FileStore.Configuration;

namespace TG_IT.ACME.Protocol.Storage.FileStore
{
    public class AccountStore : IAccountStore, IOrderStore
    {
        private readonly IOptions<FileStoreOptions> _options;

        private readonly Regex _idRegex;

        public AccountStore(IOptions<FileStoreOptions> options)
        {
            _options = options;
            _idRegex = new Regex("[\\w\\d_-]+", RegexOptions.Compiled);
        }

        public async Task<Account> LoadAccountAsync(string accountId, CancellationToken cancellationToken)
        {
            if (!_idRegex.IsMatch(accountId))
                throw new InvalidOperationException("AccountId seems invalid!");

            var accountPath = Path.Combine(_options.Value.AccountPath,
                accountId, "account.json");

            var utf8Bytes = await File.ReadAllBytesAsync(accountPath, cancellationToken);
            var result = JsonSerializer.Deserialize<Account>(utf8Bytes);

            return result;
        }

        public async Task<Order> LoadOrderAsync(string orderId, Account account, CancellationToken cancellationToken)
        {
            if (!_idRegex.IsMatch(orderId))
                throw new InvalidOperationException("AccountId seems invalid!");

            var orderPath = Path.Combine(_options.Value.AccountPath,
                account.AccountId, "orders", $"{orderId}.json");

            var utf8Bytes = await File.ReadAllBytesAsync(orderPath, cancellationToken);
            var result = JsonSerializer.Deserialize<Order>(utf8Bytes);
            result.Account = account;

            return result;
        }

        public async Task SaveAccountAsync(Account newAccount, CancellationToken cancellationToken)
        {
            var accountPath = Path.Combine(_options.Value.AccountPath,
                newAccount.AccountId, "account.json");

            Directory.CreateDirectory(Path.GetDirectoryName(accountPath));

            var utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(newAccount);
            await File.WriteAllBytesAsync(accountPath, utf8Bytes, cancellationToken);
        }

        public async Task SaveOrderAsync(Order order, CancellationToken cancellationToken)
        {
            var orderPath = Path.Combine(_options.Value.AccountPath,
                order.Account.AccountId, "orders", $"{order.OrderId}.json");

            Directory.CreateDirectory(Path.GetDirectoryName(orderPath));

            var utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(order);
            await File.WriteAllBytesAsync(orderPath, utf8Bytes, cancellationToken);
        }
    }
}
