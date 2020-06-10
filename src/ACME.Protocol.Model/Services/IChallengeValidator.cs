using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
    public interface IChallengeValidator
    {
        Task<(bool IsValid, AcmeError? error)> ValidateChallengeAsync(Challenge challenge, Account account, CancellationToken cancellationToken);
    }

    public abstract class TokenChallengeValidator : IChallengeValidator
    {
        protected abstract Task<(string? Content, AcmeError? Error)> LoadChallengeResponseAsync(Challenge challenge, CancellationToken cancellationToken);
        protected abstract string GetExpectedContent(Challenge challenge, Account account);

        public async Task<(bool IsValid, AcmeError? error)> ValidateChallengeAsync(Challenge challenge, Account account, CancellationToken cancellationToken)
        {
            //TODO: Check account state;
            //TODO: Check authorization expiry;
            //TODO: Check order expiry

            var challengeResponse = await LoadChallengeResponseAsync(challenge, cancellationToken);
            if (challengeResponse.Error != null)
                return (false, challengeResponse.Error);

            var expectedResponse = GetExpectedContent(challenge, account);
            if(expectedResponse == challengeResponse.Content)
                return (true, null);

            var error = new AcmeError("TODO", "TODO", challenge.Authorization.Identifier);
            return (false, error);
        }
    }

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

    public sealed class Http01ChallangeValidator : TokenChallengeValidator
    {
        private readonly HttpClient _httpClient;

        public Http01ChallangeValidator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override string GetExpectedContent(Challenge challenge, Account account)
        {
            var thumbprintBytes = account.Jwk.SecurityKey.ComputeJwkThumbprint();
            var thumbprint = Base64UrlEncoder.Encode(thumbprintBytes);

            var expectedContent = $"{challenge.Token}.{thumbprint}";
            return expectedContent;
        }

        protected override async Task<(string? Content, AcmeError? Error)> LoadChallengeResponseAsync(Challenge challenge, CancellationToken cancellationToken)
        {
            var challengeUrl = $"http://{challenge.Authorization.Identifier.Value}/.well-known/acme-challenge/{challenge.Token}";

            var response = await _httpClient.GetAsync(challengeUrl, cancellationToken);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = new AcmeError("TODO", "DETAILS", challenge.Authorization.Identifier);
                return (null, error);
            }

            var content = await response.Content.ReadAsStringAsync();
            return (content, null);
        }
    }
}
