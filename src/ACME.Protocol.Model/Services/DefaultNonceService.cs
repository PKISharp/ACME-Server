using ACME.Protocol.Model;
using ACME.Protocol.Model.Exceptions;
using ACME.Protocol.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public class DefaultNonceService : INonceService
    {
        private readonly INonceStore _nonceStore;

        public DefaultNonceService(INonceStore nonceStore)
        {
            _nonceStore = nonceStore;
        }

        public  async Task<AcmeNonce> CreateNonceAsync(CancellationToken cancellationToken)
        {
            var nonce = new AcmeNonce
            {
                Token = Guid.NewGuid().ToString()
            };

            await _nonceStore.SaveNonceAsync(nonce, cancellationToken);
            return nonce;
        }

        public async Task ValidateNonceAsync(string? nonce, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(nonce))
                throw new BadNonceException();

            if (!await _nonceStore.TryRemoveNonceAsync(new AcmeNonce { Token = nonce }, cancellationToken))
                throw new BadNonceException();
        }
    }
}
