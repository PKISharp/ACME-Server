using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Protocol.Model.ProtocolServices
{
    public interface INonceService
    {
        string CreateNonce();

        void ValidateNonce(string nonce);
    }
}
