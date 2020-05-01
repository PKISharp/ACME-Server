using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TG_IT.ACME.Protocol.Model;
using TG_IT.ACME.Protocol.Storage;

namespace TG_IT.ACME.Protocol.Services
{
    public class DefaultNonceService : INonceService
    {
        private readonly INonceStore _nonceStore;
        private readonly ILogger<DefaultNonceService> _logger;

        public DefaultNonceService(INonceStore nonceStore, ILogger<DefaultNonceService> logger)
        {
            _nonceStore = nonceStore;
            _logger = logger;
        }

        public  async Task<Nonce> CreateNonceAsync(CancellationToken cancellationToken)
        {
            var nonce = new Nonce
            {
                Token = Guid.NewGuid().ToString()
            };

            await _nonceStore.SaveNonceAsync(nonce, cancellationToken);
            _logger.LogInformation($"Created and saved new nonce: {nonce.Token}.");

            return nonce;
        }

        public async Task ValidateNonceAsync(string? nonce, CancellationToken cancellationToken)
        {
            
        }
    }
}
