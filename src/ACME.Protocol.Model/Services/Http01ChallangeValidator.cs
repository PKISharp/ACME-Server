using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
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

            //TODO: Add try / catch
            var response = await _httpClient.GetAsync(challengeUrl, cancellationToken);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //TODO: Add error message
                var error = new AcmeError("TODO", "DETAILS", challenge.Authorization.Identifier);
                return (null, error);
            }

            var content = await response.Content.ReadAsStringAsync();
            return (content, null);
        }
    }
}
