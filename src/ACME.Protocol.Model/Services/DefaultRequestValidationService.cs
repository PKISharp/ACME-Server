using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.HttpModel.Requests;
using TGIT.ACME.Protocol.Model;
using TGIT.ACME.Protocol.Model.Exceptions;
using TGIT.ACME.Protocol.Storage;

namespace TGIT.ACME.Protocol.Services
{
    public class DefaultRequestValidationService : IRequestValidationService
    {
        private readonly IAccountService _accountService;
        private readonly INonceStore _nonceStore;

        private readonly ILogger<DefaultRequestValidationService> _logger;

        private readonly string[] _supportedAlgs = new[] { "RS256" };

        public DefaultRequestValidationService(IAccountService accountService, INonceStore nonceStore,
            ILogger<DefaultRequestValidationService> logger)
        {
            _accountService = accountService;
            _nonceStore = nonceStore;
            _logger = logger;
        }

        public Task ValidateRequestHeaderAsync(AcmeHttpRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Attempting to validate AcmeHeader");
            var header = request.Header;

            if (!_supportedAlgs.Contains(header.Alg))
                throw new BadSignatureAlgorithmException();

            if (header.Jwk != null && header.Kid != null)
                throw new MalformedRequestException("Do not provide both Jwk and Kid.");
            if (header.Jwk == null && header.Kid == null)
                throw new MalformedRequestException("Provide either Jwk or Kid.");

            return Task.CompletedTask;
        }

        public async Task ValidateNonceAsync(string? nonce, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(nonce))
            {
                _logger.LogInformation($"Nonce was empty.");
                throw new BadNonceException();
            }

            if (!await _nonceStore.TryRemoveNonceAsync(new Nonce { Token = nonce }, cancellationToken))
            {
                _logger.LogInformation($"Nonce could not be located.");
                throw new BadNonceException();
            }
        }

        public async Task ValidateSignatureAsync(AcmeHttpRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Header.Jwk == null && request.Header.Kid == null)
                throw new MalformedRequestException("Either provide JWK or KID");

            var jwk = request.Header.Jwk;
            if(jwk == null)
            {
                try
                {
                    var accountId = request.Header.GetAccountId();
                    var account = await _accountService.LoadAcountAsync(accountId, cancellationToken);
                    jwk = account?.Jwk;
                } catch (InvalidOperationException)
                {
                    throw new MalformedRequestException("KID could not be found.");
                }
            }

            if(jwk == null)
                throw new MalformedRequestException("Could not load JWK.");

            var securityKey = jwk.JwkSecurityKey();
            
            using var signatureProvider = new AsymmetricSignatureProvider(securityKey, request.Header.Alg);
            var plainText = System.Text.Encoding.UTF8.GetBytes($"{request.EncodedHeader}.{request.EncodedPayload}");
            var signature = Base64UrlEncoder.DecodeBytes(request.Signature);

            if (!signatureProvider.Verify(plainText, signature))
                throw new MalformedRequestException("The signature could not be verified");
        }
    }
}
