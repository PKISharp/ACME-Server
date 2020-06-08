using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TGIT.ACME.Protocol.Workers
{
    public interface IIssuanceWorker
    {
        Task RunAsync(CancellationToken cancellationToken);
    }

    public class IssuanceWorker : IIssuanceWorker
    {
        public Task RunAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
