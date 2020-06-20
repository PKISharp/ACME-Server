using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.HttpModel.Requests;
using TGIT.ACME.Protocol.Infrastructure;
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

        public Task ValidateRequestHeaderAsync(AcmePostRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogDebug("Attempting to validate AcmeHeader ...");
            var header = request.Header;

            if (!_supportedAlgs.Contains(header.Value.Alg))
                throw new BadSignatureAlgorithmException();

            if (header.Value.Jwk != null && header.Value.Kid != null)
                throw new MalformedRequestException("Do not provide both Jwk and Kid.");
            if (header.Value.Jwk == null && header.Value.Kid == null)
                throw new MalformedRequestException("Provide either Jwk or Kid.");

            _logger.LogDebug("successfully validated AcmeHeader.");
            return Task.CompletedTask;
        }

        public async Task ValidateNonceAsync(string? nonce, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Attempting to validate replay nonce ...");
            if (string.IsNullOrWhiteSpace(nonce))
            {
                _logger.LogDebug($"Nonce was empty.");
                throw new BadNonceException();
            }

            if (!await _nonceStore.TryRemoveNonceAsync(new Nonce(nonce), cancellationToken))
            {
                _logger.LogDebug($"Nonce was invalid.");
                throw new BadNonceException();
            }

            _logger.LogDebug("successfully validated replay nonce.");
        }

        public async Task ValidateSignatureAsync(AcmePostRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Attempting to validate signature ...");
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Header.Value.Jwk == null && request.Header.Value.Kid == null)
                throw new MalformedRequestException("Either provide JWK or KID");

            var jwk = request.Header.Value.Jwk;
            if(jwk == null)
            {
                try
                {
                    var accountId = request.Header.Value.GetAccountId();
                    var account = await _accountService.LoadAcountAsync(accountId, cancellationToken);
                    jwk = account?.Jwk;
                } catch (InvalidOperationException)
                {
                    throw new MalformedRequestException("KID could not be found.");
                }
            }

            if(jwk == null)
                throw new MalformedRequestException("Could not load JWK.");

            var securityKey = jwk.SecurityKey;
            
            using var signatureProvider = new AsymmetricSignatureProvider(securityKey, request.Header.Value.Alg);
            var plainText = System.Text.Encoding.UTF8.GetBytes($"{request.Header.EncodedJson}.{request.Payload?.EncodedJson ?? ""}");
            var signature = Base64UrlEncoder.DecodeBytes(request.Signature);

            if (!signatureProvider.Verify(plainText, signature))
                throw new MalformedRequestException("The signature could not be verified");

            _logger.LogDebug("successfully validated signature.");
        }
    }
}
