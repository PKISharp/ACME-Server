using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
    public interface IChallengeValidator
    {
        Task<bool> ValidateChallengeAsync(Challenge challenge, Account account, CancellationToken cancellationToken);
    }

    public abstract class TokenChallengeValidator : IChallengeValidator
    {
        protected abstract Task<string> LoadChallengeResponseAsync(Challenge challenge);

        private async Task<bool> ValidateChallengeAsync(Challenge challenge, Account account, string challengeResponse, CancellationToken cancellationToken)
        {
            return false;
        }

        public Task<bool> ValidateChallengeAsync(Challenge challenge, Account account, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Dns01ChallangeValidator : TokenChallengeValidator
    {
        protected override Task<string> LoadChallengeResponseAsync(Challenge challenge)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Http01ChallangeValidator : TokenChallengeValidator
    {
        protected override Task<string> LoadChallengeResponseAsync(Challenge challenge)
        {
            throw new System.NotImplementedException();
        }
    }
}
