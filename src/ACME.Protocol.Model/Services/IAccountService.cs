using ACME.Server.HttpModel.Requests;
using ACME.Server.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TG_IT.ACME.Protocol.Services
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
