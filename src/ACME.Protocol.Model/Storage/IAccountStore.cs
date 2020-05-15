using System.Threading;
using System.Threading.Tasks;
using TG_IT.ACME.Protocol.Model;

namespace TG_IT.ACME.Protocol.Storage
{
    public interface IAccountStore
    {
        Task SaveAccountAsync(Account account, CancellationToken cancellationToken);
        Task<Account?> LoadAccountAsync(string accountId, CancellationToken cancellationToken);
    }
}
