using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TG_IT.ACME.Protocol.HttpModel
{
    public class Order
    {
        public Order(Model.Order model,
            IEnumerable<string> authorizationUrls, string finalizeUrl, string certificateUrl)
        {
            if (model is null)
                throw new System.ArgumentNullException(nameof(model));

            if (authorizationUrls is null)
                throw new System.ArgumentNullException(nameof(authorizationUrls));

            if (string.IsNullOrEmpty(finalizeUrl))
                throw new System.ArgumentNullException(nameof(finalizeUrl));

            if (string.IsNullOrEmpty(certificateUrl))
                throw new System.ArgumentNullException(nameof(certificateUrl));

            Status = model.Status.ToString().ToLowerInvariant();
            
            Expires = model.Expires?.ToString("o", CultureInfo.InvariantCulture);
            NotBefore = model.NotBefore?.ToString("o", CultureInfo.InvariantCulture);
            NotAfter = model.NotAfter?.ToString("o", CultureInfo.InvariantCulture);

            Identifiers = model.Identifiers.Select(x => new Identifier(x)).ToList();

            Authorizations = new List<string>(authorizationUrls);
            Finalize = finalizeUrl;
            Certificate = certificateUrl;

            Error = model.Error;
        }

        public string Status { get; set; }

        public List<Identifier> Identifiers { get; set; }

        public string? Expires { get; set; }
        public string? NotBefore { get; set; }
        public string? NotAfter { get; set; }

        public Error? Error { get; set; }

        public List<string> Authorizations { get; set; }
        public string Finalize { get; set; }
        public string? Certificate { get; set; }
    }
}
