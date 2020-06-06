using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
    public interface IChallengeValidationService
    {
        Task<Challenge> ValidateChallengeAsync(Account account, string orderId, string authzId, string challengeId, CancellationToken cancellationToken);
        Task ValidateChallengeAsync(Challenge challenge, CancellationToken cancellationToken);
    }
}
