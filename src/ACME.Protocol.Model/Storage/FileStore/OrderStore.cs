using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;
using TGIT.ACME.Protocol.Model.Exceptions;
using TGIT.ACME.Protocol.Storage.FileStore.Configuration;

namespace TGIT.ACME.Protocol.Storage.FileStore
{
    public class OrderStore : StoreBase, IOrderStore
    {
        public OrderStore(IOptions<FileStoreOptions> options)
            : base(options)
        { }

        public async Task<Order?> LoadOrderAsync(string orderId, Account account, CancellationToken cancellationToken)
        {
            if (account is null)
                throw new ArgumentNullException(nameof(account));
            if (!IdentifierRegex.IsMatch(orderId))
                throw new MalformedRequestException("OrderId does not match expected format.");

            var orderPath = Path.Combine(Options.Value.AccountPath,
                account.AccountId, "orders", $"{orderId}.json");

            if (!File.Exists(orderPath))
                return null;

            using (var fileStream = File.OpenRead(orderPath))
            {
                var utf8Bytes = new byte[fileStream.Length];
                await fileStream.ReadAsync(utf8Bytes, cancellationToken);
                var result = JsonSerializer.Deserialize<Order>(utf8Bytes);
                
                result.Account = account;

                return result;
            }
        }

        public async Task SaveOrderAsync(Order setOrder, CancellationToken cancellationToken)
        {
            if (setOrder is null)
                throw new ArgumentNullException(nameof(setOrder));

            var orderPath = Path.Combine(Options.Value.AccountPath,
                setOrder.Account.AccountId, "orders", $"{setOrder.OrderId}.json");

            Directory.CreateDirectory(Path.GetDirectoryName(orderPath));

            using (var fileStream = File.Open(orderPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                var existingOrder = await LoadOrderAsync(setOrder.OrderId, setOrder.Account, cancellationToken);
                if (existingOrder != null && existingOrder.Version != setOrder.Version)
                    throw new ConcurrencyException();

                setOrder.Version = DateTime.UtcNow.Ticks;

                var utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(setOrder);
                await fileStream.WriteAsync(utf8Bytes, cancellationToken);
            }
        }

        public Task<List<Order>> GetValidatableOrders(CancellationToken cancellationToken)
        {
            var workPath = Path.Combine(Options.Value.WorkingPath, "validateable");
            var files = Directory.EnumerateFiles(workPath);

            var result = new List<Order>();
            foreach(var file in files)
            {
                var orderPath = 
            }

            return result;
        }

        public Task<List<Order>> GetFinalizableOrders(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
