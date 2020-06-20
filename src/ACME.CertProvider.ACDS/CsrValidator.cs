using CERTENROLLLib;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol;
using TGIT.ACME.Protocol.Model;
using TGIT.ACME.Protocol.Services;

namespace TGIT.ACME.CertProvider.ACDS
{
    public class CsrValidator : ICsrValidator
    {
        private readonly IOptions<AcmeProtocolOptions> _options;
        private readonly ILogger<CsrValidator> _logger;

        public CsrValidator(IOptions<AcmeProtocolOptions> options, ILogger<CsrValidator> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task<(bool isValid, AcmeError? error)> ValidateCsrAsync(Order order, string csr, CancellationToken cancellationToken)
        {
            try
            {
                var request = new CX509CertificateRequestPkcs10();

                request.InitializeDecode(csr, EncodingType.XCN_CRYPT_STRING_BASE64_ANY);
                request.CheckSignature();

                if (!SubjectIsValid(request, order))
                    return (false, new AcmeError("TYPE", "CN Invalid"));

                if (!SubjectAlternateNamesAreValid(request, order))
                    return (false, new AcmeError("TYPE", "SAN Invalid"));

                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
            }

            return (false, new AcmeError("TYPE", "Generic Error"));
        }

        private bool SubjectIsValid(CX509CertificateRequestPkcs10 request, Order order)
        {
            var validCNs = order.Identifiers.Select(x => x.Value)
                .Concat(order.Identifiers.Where(x => x.IsWildcard).Select(x => x.Value.Substring(2)))
                .Select(x => "CN=" + x)
                .ToList();

            return validCNs.Any(x => request.Subject.Name.Equals(x) || 
                (_options.Value.AllowCNSuffix && request.Subject.Name.StartsWith(x)));
        }

        private bool SubjectAlternateNamesAreValid(CX509CertificateRequestPkcs10 request, Order order)
        {
            return true;
        }
    }
}
