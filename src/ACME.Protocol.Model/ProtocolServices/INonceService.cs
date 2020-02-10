using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Protocol.Model.ProtocolServices
{
    public interface INonceService
    {
        Task<string> CreateNonceAsync();

        Task ValidateNonceAsync(string nonce);
    }
}
