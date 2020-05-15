using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TG_IT.ACME.Protocol.HttpModel.Requests;
using TG_IT.ACME.Protocol.Model;

namespace TG_IT.ACME.Protocol.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(Jwk jwk, List<string>? contact,
            bool termsOfServiceAgreed, CancellationToken cancellationToken);

        Task<Account?> FindAccountAsync(Jwk jwk, CancellationToken cancellationToken);

        Task<Account?> LoadAcountAsync(string accountId, CancellationToken cancellationToken);

        Task<Account> FromRequestAsync(AcmeHttpRequest request, CancellationToken cancellationToken);
    }
}
