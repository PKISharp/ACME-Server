using DnsClient;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
    public sealed class Dns01ChallangeValidator : TokenChallengeValidator
    {
        private readonly ILogger<Dns01ChallangeValidator> _logger;

        public Dns01ChallangeValidator(ILogger<Dns01ChallangeValidator> logger)
        {
            _logger = logger;
        }

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

        protected override async Task<(string? Content, AcmeError? Error)> LoadChallengeResponseAsync(Challenge challenge, CancellationToken cancellationToken)
        {
            try
            {
                var dnsClient = new LookupClient();
                var dnsBaseUrl = challenge.Authorization.Identifier.Value.Replace("*.", "", StringComparison.OrdinalIgnoreCase);
                var dnsRecordName = $"_acme-challenge.{dnsBaseUrl}";

                var dnsResponse = await dnsClient.QueryAsync(dnsRecordName, QueryType.TXT);
                var content = dnsResponse.Answers.TxtRecords()?.FirstOrDefault()?.Text.FirstOrDefault();

                if (string.IsNullOrWhiteSpace(content))
                    return (null, new AcmeError("TODO", "Empty TXT Record"));

                return (content, null);
            } 
            catch (Exception ex)
            {
                _logger.LogInformation("DNS-Lookup failed: " + ex.Message);
                return (null, new AcmeError("TODO", "Generic Error"));
            }
        }
    }
}
