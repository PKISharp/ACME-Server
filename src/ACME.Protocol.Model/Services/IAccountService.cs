using ACME.Protocol.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public interface IAccountService
    {
        Task<AcmeAccount> CreateAccountAsync(KeyWrapper? keys, List<string>? contact,
            bool termsOfServiceAgreed, CancellationToken requestAborted);
    }
}
