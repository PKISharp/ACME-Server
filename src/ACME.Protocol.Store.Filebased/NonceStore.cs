using ACME.Protocol.Model;
using ACME.Protocol.Storage;
using System;
using System.Threading.Tasks;

namespace ACME.Protocol.Store.Filebased
{
    public class NonceStore : INonceStore
    {
        public Task SaveNonce(AcmeNonce nonce)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryRemoveNonce(AcmeNonce nonce)
        {
            throw new NotImplementedException();
        }
    }
}
