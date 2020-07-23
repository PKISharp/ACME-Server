using System.Collections.Generic;

namespace TGIT.ACME.Protocol.HttpModel
{
    public class Account
    {
        public Account(Model.Account model, string ordersUrl)
        {
            if (model is null)
                throw new System.ArgumentNullException(nameof(model));

            Status = model.Status.ToString().ToLowerInvariant();

            Contact = model.Contacts;
            TermsOfServiceAgreed = model.TOSAccepted.HasValue;

            ExternalAccountBinding = null;
            Orders = ordersUrl;
        }

        public string Status { get; set; }
        public string Orders { get; set; }

        public List<string>? Contact { get; set; }
        public bool? TermsOfServiceAgreed { get; set; }

        public object? ExternalAccountBinding { get; set; }
    }
}
