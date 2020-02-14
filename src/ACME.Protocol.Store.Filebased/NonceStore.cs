using ACME.Protocol.Model;
using ACME.Protocol.Storage;
using ACME.Protocol.Store.Filebased.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Store.Filebased
{
    public class NonceStore : INonceStore
    {
        private readonly IOptions<FileStoreOptions> _options;

        public NonceStore(IOptions<FileStoreOptions> options)
        {
            _options = options;
        }

        public async Task SaveNonceAsync(AcmeNonce nonce, CancellationToken cancellationToken)
        {
            var noncePath = System.IO.Path.Combine(_options.Value.NoncePath, nonce.Token);
            await System.IO.File.WriteAllTextAsync(noncePath, DateTime.Now.ToString("o"), cancellationToken);
        }

        public Task<bool> TryRemoveNonceAsync(AcmeNonce nonce, CancellationToken cancellationToken)
        {
            var noncePath = System.IO.Path.Combine(_options.Value.NoncePath, nonce.Token);
            if (!System.IO.File.Exists(noncePath))
                return Task.FromResult(false);

            System.IO.File.Delete(noncePath);
            return Task.FromResult(true);
        }
    }
}
