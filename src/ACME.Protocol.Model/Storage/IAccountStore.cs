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
        Task SaveAccountAsync(Account account, CancellationToken cancellationToken);
        Task<Account> LoadAccountAsync(string accountId, CancellationToken cancellationToken);
    }
}
