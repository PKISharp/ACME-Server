using System;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
    public sealed class Dns01ChallangeValidator : TokenChallengeValidator
    {
        protected override string GetExpectedContent(Challenge challenge, Account account)
        {
            throw new NotImplementedException();
        }

        protected override Task<(string? Content, AcmeError? Error)> LoadChallengeResponseAsync(Challenge challenge, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
