using ACME.Protocol.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public interface IAccountService
    {
        Task<AcmeAccount> CreateAccountAsync(Jwk jwk, List<string> contact,
            bool termsOfServiceAgreed, CancellationToken cancellationToken);

        Task<AcmeAccount> FindAccountAsync(Jwk jwk, CancellationToken cancellationToken);

        Task<AcmeAccount> LoadAcountAsync(string accountId, CancellationToken cancellationToken);
    }
}
