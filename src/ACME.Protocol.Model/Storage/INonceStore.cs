using ACME.Protocol.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Protocol.Storage
{
    public interface INonceStore
    {
        Task SaveNonce(AcmeNonce nonce);
        Task<bool> TryRemoveNonce(AcmeNonce nonce);
    }
}
