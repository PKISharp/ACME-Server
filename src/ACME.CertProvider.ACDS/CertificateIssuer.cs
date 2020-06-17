using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Services;
using CertEnroll = CERTENROLLLib;

namespace TGIT.ACME.CertProvider.ACDS
{
    public sealed class CertificateIssuer : ICertificateIssuer
    {
        public Task<byte[]> IssueCertificate(string csr, CancellationToken cancellationToken)
        {
            var request = new CertEnroll.CX509CertificateRequestPkcs10Class();
            request.InitializeDecode(csr, CertEnroll.EncodingType.XCN_CRYPT_STRING_BASE64_ANY);

            var enrollment = new CertEnroll.CX509EnrollmentClass();
            enrollment.InitializeFromRequest(request);
            enrollment.Enroll();
            var certificate = enrollment.Certificate[CertEnroll.EncodingType.XCN_CRYPT_STRING_BASE64_ANY];

            return new byte[];
        }
    }
}
