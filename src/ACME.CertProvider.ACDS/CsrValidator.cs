using CERTENROLLLib;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;
using TGIT.ACME.Protocol.Services;

namespace TGIT.ACME.CertProvider.ACDS
{
    public class CsrValidator : ICsrValidator
    {
        public Task<bool> ValidateCsrAsync(Order order, string csr, CancellationToken cancellationToken)
        {
            var request = new CX509CertificateRequestPkcs10();
            request.InitializeDecode(csr, EncodingType.XCN_CRYPT_STRING_BASE64_ANY);
            request.CheckSignature();

            // TODO : Validate subject
            // TODO : Validate SAN

            return Task.FromResult(true);
        }
    }
}
