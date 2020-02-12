using ACME.Protocol.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public class DefaultNonceService : INonceService
    {
        public Task<AcmeNonce> CreateNonceAsync(AcmeRequestContext context)
        {
            throw new NotImplementedException();
        }

        public Task ValidateNonceAsync(string nonce)
        {
            throw new NotImplementedException();
        }
    }
}
