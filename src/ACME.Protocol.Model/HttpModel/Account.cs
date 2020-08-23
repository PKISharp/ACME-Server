using System.Collections.Generic;

namespace TGIT.ACME.Protocol.HttpModel
{
    /// <summary>
    /// 
    /// </summary>
    public class Account
    {
        public Account()
        {
            Status = EnumMappings.GetEnumString(Model.AccountStatus.Valid);
        }

        public Account(Model.Account model, string ordersUrl)
        {
            if (model is null)
                throw new System.ArgumentNullException(nameof(model));

            Status = EnumMappings.GetEnumString(model.Status);

            Contact = model.Contacts;
            TermsOfServiceAgreed = model.TOSAccepted.HasValue;

            ExternalAccountBinding = null;
            Orders = ordersUrl;
        }

        public string Status { get; set; }
        public string? Orders { get; set; }

        public List<string>? Contact { get; set; }
        public bool? TermsOfServiceAgreed { get; set; }

        public object? ExternalAccountBinding { get; set; }
    }
}
