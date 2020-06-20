using System;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Services;
using CertEnroll = CERTENROLLLib;
using CertCli = CERTCLILib;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.CertProvider.ACDS
{
    public sealed class CertificateIssuer : ICertificateIssuer
    {
        public Task<(byte[]? certificate, AcmeError? error)> IssueCertificate(string csr, CancellationToken cancellationToken)
        {
            try
            {
                
            } catch (Exception ex)
            {

            }

            return Task.FromResult((new byte[0], (AcmeError)null));
        }
    }
}
