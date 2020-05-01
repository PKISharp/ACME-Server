using ACME.Server.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TG_IT.ACME.Protocol.Storage
{
    public interface INonceStore
    {
        Task SaveNonceAsync(Nonce nonce, CancellationToken cancellationToken);
        Task<bool> TryRemoveNonceAsync(Nonce nonce, CancellationToken cancellationToken);
    }
}
