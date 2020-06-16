using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;
using TGIT.ACME.Protocol.Services;

namespace TGIT.ACME.CertProvider.ACDS
{
    public class CsrValidator : ICsrValidator
    {
        public Task ValidateCsr(Order order, string csr, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
