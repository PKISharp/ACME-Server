using ACME.Protocol.Model;
using ACME.Protocol.Model.Exceptions;
using ACME.Protocol.Model.Model;
using ACME.Protocol.Storage;
using System;
using System.Collections.Generic;
using System.Text;
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

        public  async Task<AcmeNonce> CreateNonceAsync(AcmeRequestContext context, CancellationToken cancellationToken)
        {
            var nonce = new AcmeNonce
            {
                Token = Guid.NewGuid().ToString()
            };

            await _nonceStore.SaveNonceAsync(nonce, cancellationToken);
            return nonce;
        }

        public async Task ValidateNonceAsync(AcmeRequestContext context, CancellationToken cancellationToken)
        {
            if (!await _nonceStore.TryRemoveNonceAsync(context.Nonce, cancellationToken))
                throw new BadNonceException();
        }
    }
}
