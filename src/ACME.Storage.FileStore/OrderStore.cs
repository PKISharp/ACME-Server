using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;
using TGIT.ACME.Protocol.Model.Exceptions;
using TGIT.ACME.Protocol.Storage;
using TGIT.ACME.Storage.FileStore.Configuration;

namespace TGIT.ACME.Storage.FileStore
{
    public class OrderStore : StoreBase, IOrderStore
    {
        public OrderStore(IOptions<FileStoreOptions> options)
            : base(options)
        { }

        public async Task<Order?> LoadOrderAsync(string orderId, CancellationToken cancellationToken)
        {
            if (!IdentifierRegex.IsMatch(orderId))
                throw new MalformedRequestException("OrderId does not match expected format.");

            var orderPath = Path.Combine(Options.Value.OrderPath, $"{orderId}.json");

            if (!File.Exists(orderPath))
                return null;

            using (var fileStream = File.OpenRead(orderPath))
            {
                var utf8Bytes = new byte[fileStream.Length];
                await fileStream.ReadAsync(utf8Bytes, cancellationToken);
                var result = JsonConvert.DeserializeObject<Order>(Encoding.UTF8.GetString(utf8Bytes));

                return result;
            }
        }

        public async Task SaveOrderAsync(Order setOrder, CancellationToken cancellationToken)
        {
            if (setOrder is null)
                throw new ArgumentNullException(nameof(setOrder));

            var orderFilePath = Path.Combine(Options.Value.OrderPath, $"{setOrder.OrderId}.json");

            Directory.CreateDirectory(Path.GetDirectoryName(orderFilePath));

            await CreateOwnerFileAsync(setOrder, cancellationToken);
            await WriteWorkFilesAsync(setOrder, cancellationToken);

            using (var fileStream = File.Open(orderFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                var existingOrder = await LoadOrderAsync(setOrder.OrderId, cancellationToken);
                if (existingOrder != null && existingOrder.Version != setOrder.Version)
                    throw new ConcurrencyException();

                setOrder.Version = DateTime.UtcNow.Ticks;

                var utf8Bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(setOrder, JsonDefaults.Settings));
                await fileStream.WriteAsync(utf8Bytes, cancellationToken);
            }
        }

        private async Task CreateOwnerFileAsync(Order order, CancellationToken cancellationToken)
        {
            var ownerFilePath = Path.Combine(Options.Value.AccountPath, order.AccountId, "orders", order.OrderId);
            if (!File.Exists(ownerFilePath)) {
                await File.WriteAllTextAsync(ownerFilePath, 
                    order.Expires?.ToString("o", CultureInfo.InvariantCulture), 
                    cancellationToken);
            }
        }

        private async Task WriteWorkFilesAsync(Order order, CancellationToken cancellationToken)
        {
            var validationFilePath = Path.Combine(Options.Value.WorkingPath, "validate", order.OrderId);
            if (order.Authorizations.Any(a => a.Challenges.Any(c => c.Status == ChallengeStatus.Processing)))
            {
                if (!File.Exists(validationFilePath)) {
                    await File.WriteAllTextAsync(validationFilePath, 
                        order.Authorizations.Min(a => a.Expires)?.ToString("o", CultureInfo.InvariantCulture),
                        cancellationToken);
                }
            } 
            else if (File.Exists(validationFilePath))
            {
                File.Delete(validationFilePath);
            }

            var processingFilePath = Path.Combine(Options.Value.WorkingPath, "process", order.OrderId);
            if(order.Status == OrderStatus.Processing)
            {
                if (!File.Exists(processingFilePath)) {
                    await File.WriteAllTextAsync(processingFilePath, 
                        order.Expires?.ToString("o", CultureInfo.InvariantCulture),
                        cancellationToken);
                }
            }
            else if (File.Exists(processingFilePath))
            {
                File.Delete(processingFilePath);
            }
        }

        public async Task<List<Order>> GetValidatableOrders(CancellationToken cancellationToken)
        {
            var workPath = Path.Combine(Options.Value.WorkingPath, "validate");
            var files = Directory.EnumerateFiles(workPath);

            var result = new List<Order>();
            foreach(var filePath in files)
            {
                try
                {
                    var orderId = Path.GetFileName(filePath);
                    var order = await LoadOrderAsync(orderId, cancellationToken);

                    if(order != null)
                        result.Add(order);
                }
                catch {
                    //TODO: Write log message
                }
            }

            return result;
        }

        public async Task<List<Order>> GetFinalizableOrders(CancellationToken cancellationToken)
        {
            var workPath = Path.Combine(Options.Value.WorkingPath, "process");
            var files = Directory.EnumerateFiles(workPath);

            var result = new List<Order>();
            foreach (var filePath in files)
            {
                try
                {
                    var orderId = Path.GetFileName(filePath);
                    var order = await LoadOrderAsync(orderId, cancellationToken);

                    if (order != null)
                        result.Add(order);
                }
                catch {
                    //TODO: Write log message
                }
            }

            return result;
        }
    }
}
