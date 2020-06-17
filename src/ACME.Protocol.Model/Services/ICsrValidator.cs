using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
    public interface ICsrValidator
    {
        Task<bool> ValidateCsrAsync(Order order, string csr, CancellationToken cancellationToken);
    }

    public interface ICertificateIssuer
    {
        Task<byte[]> IssueCertificate(string csr, CancellationToken cancellationToken);
    }
}
