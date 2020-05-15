using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using TG_IT.ACME.Protocol.Model;
using TG_IT.ACME.Protocol.Storage.FileStore.Configuration;

namespace TG_IT.ACME.Protocol.Storage.FileStore
{
    public class NonceStore : INonceStore
    {
        private readonly IOptions<FileStoreOptions> _options;

        public NonceStore(IOptions<FileStoreOptions> options)
        {
            _options = options;
        }

        public async Task SaveNonceAsync(Nonce nonce, CancellationToken cancellationToken)
        {
            if (nonce is null)
                throw new ArgumentNullException(nameof(nonce));

            var noncePath = System.IO.Path.Combine(_options.Value.NoncePath, nonce.Token);
            await System.IO.File.WriteAllTextAsync(noncePath, DateTime.Now.ToString("o", CultureInfo.InvariantCulture), cancellationToken);
        }

        public Task<bool> TryRemoveNonceAsync(Nonce nonce, CancellationToken cancellationToken)
        {
            if (nonce is null)
                throw new ArgumentNullException(nameof(nonce));

            var noncePath = System.IO.Path.Combine(_options.Value.NoncePath, nonce.Token);
            if (!System.IO.File.Exists(noncePath))
                return Task.FromResult(false);

            System.IO.File.Delete(noncePath);
            return Task.FromResult(true);
        }
    }
}
