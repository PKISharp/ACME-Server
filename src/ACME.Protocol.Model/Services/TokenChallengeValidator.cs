using System;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
    public abstract class TokenChallengeValidator : IChallengeValidator
    {
        protected abstract Task<(string? Content, AcmeError? Error)> LoadChallengeResponseAsync(Challenge challenge, CancellationToken cancellationToken);
        protected abstract string GetExpectedContent(Challenge challenge, Account account);

        public async Task<(bool IsValid, AcmeError? error)> ValidateChallengeAsync(Challenge challenge, Account account, CancellationToken cancellationToken)
        {
            return (true, null);

            // TODO : Enable code!

            if (challenge is null)
                throw new ArgumentNullException(nameof(challenge));
            if (account is null)
                throw new ArgumentNullException(nameof(account));

            //TODO: Check account state;
            //TODO: Check authorization expiry;
            //TODO: Check order expiry

            var (challengeContent, error) = await LoadChallengeResponseAsync(challenge, cancellationToken);
            if (error != null)
                return (false, error);

            var expectedResponse = GetExpectedContent(challenge, account);
            if(expectedResponse != challengeContent)
                return (false, new AcmeError("TODO", "TODO", challenge.Authorization.Identifier));

            return (true, null);
        }
    }
}
