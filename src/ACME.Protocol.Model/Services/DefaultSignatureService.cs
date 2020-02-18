using ACME.Protocol.HttpModel.Requests;
using ACME.Protocol.Model.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public class DefaultSignatureService : IAcmeSignatureService
    {
        private readonly IAccountService _accountService;

        public DefaultSignatureService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task ValidateAcmeRequestAsync(AcmeHttpRequest request, CancellationToken cancellationToken)
        {
            var jwk = request.Header.Jwk;
            if(jwk == null)
            {
                var accountId = request.Header.GetAccountId();
                var account = await _accountService.LoadAcountAsync(accountId, cancellationToken);
                jwk = account.Jwk;
            }

            var securityKey = jwk!.GetJwkSecurityKey();
            
            var signatureProvider = new AsymmetricSignatureProvider(securityKey, request.Header.Alg);
            var plainText = System.Text.Encoding.UTF8.GetBytes($"{request.EncodedHeader}.{request.EncodedPayload}");
            var signature = Base64UrlEncoder.DecodeBytes(request.Signature);

            if (!signatureProvider.Verify(plainText, signature))
                throw new MalformedRequestException("The signature could not be verified");
        }
    }
}
