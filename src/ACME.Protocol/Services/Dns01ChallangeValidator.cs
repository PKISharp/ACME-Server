using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
    public sealed class Dns01ChallangeValidator : TokenChallengeValidator
    {
        protected override string GetExpectedContent(Challenge challenge, Account account)
        {
            using var sha256 = SHA256.Create();

            var thumbprintBytes = account.Jwk.SecurityKey.ComputeJwkThumbprint();
            var thumbprint = Base64UrlEncoder.Encode(thumbprintBytes);

            var keyAuthBytes = Encoding.UTF8.GetBytes($"{challenge.Token}.{thumbprint}");
            var digestBytes = sha256.ComputeHash(keyAuthBytes);

            var digest = Base64UrlEncoder.Encode(digestBytes);
            return digest;
        }

        protected override Task<(string? Content, AcmeError? Error)> LoadChallengeResponseAsync(Challenge challenge, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
