using ACME.Protocol.Model;
using ACME.Protocol.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public interface INonceService
    {
        Task<AcmeNonce> CreateNonceAsync(AcmeRequestContext context);

        Task ValidateNonceAsync(string nonce);
    }
}
