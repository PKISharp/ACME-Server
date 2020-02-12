using ACME.Protocol.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Storage
{
    public interface INonceStore
    {
        Task SaveNonce(AcmeNonce nonce, CancellationToken cancellationToken);
        Task<bool> TryRemoveNonce(AcmeNonce nonce, CancellationToken cancellationToken);
    }
}
