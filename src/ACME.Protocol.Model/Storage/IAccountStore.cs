using ACME.Protocol.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACME.Protocol.Storage
{
    public interface IAccountStore
    {
        Task SaveAccountAsync(AcmeAccount newAccount, CancellationToken cancellationToken);
        Task<AcmeAccount> LoadAccountAsync(string accountId, CancellationToken cancellationToken);
    }
}
