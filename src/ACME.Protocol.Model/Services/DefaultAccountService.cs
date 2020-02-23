using ACME.Protocol.HttpModel.Requests;
using ACME.Protocol.Model;
using ACME.Protocol.Model.Exceptions;
using ACME.Protocol.Storage;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Services
{
    public class DefaultAccountService : IAccountService
    {
        private readonly IAccountStore _accountStore;

        public DefaultAccountService(IAccountStore accountStore)
        {
            _accountStore = accountStore;
        }

        public async Task<Account> CreateAccountAsync(Jwk jwk, List<string> contact,
            bool termsOfServiceAgreed, CancellationToken cancellationToken)
        {
            var newAccount = new Account
            {
                Jwk = jwk,
                Contact = contact,
                TOSAccepted = termsOfServiceAgreed ? DateTimeOffset.Now : (DateTimeOffset?)null
            };

            await _accountStore.SaveAccountAsync(newAccount, cancellationToken);
            return newAccount;
        }

        public Task<Account> FindAccountAsync(Jwk jwk, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Account> FromRequestAsync(AcmeHttpRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request?.Header.Kid))
                throw new MalformedRequestException("Kid header is missing");

            //TODO: Get accountId from Kid?
            var accountId = request.Header.GetAccountId();
            return LoadAcountAsync(accountId, cancellationToken);
        }

        public async Task<Account> LoadAcountAsync(string accountId, CancellationToken cancellationToken)
        {
            return await _accountStore.LoadAccountAsync(accountId, cancellationToken);
        }
    }
}
