using ACME.Protocol.HttpModel.Requests;
using ACME.Protocol.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(Jwk jwk, List<string> contact,
            bool termsOfServiceAgreed, CancellationToken cancellationToken);

        Task<Account> FindAccountAsync(Jwk jwk, CancellationToken cancellationToken);

        Task<Account> LoadAcountAsync(string accountId, CancellationToken cancellationToken);

        Task<Account> FromRequestAsync(AcmeHttpRequest request, CancellationToken cancellationToken);
    }
}
