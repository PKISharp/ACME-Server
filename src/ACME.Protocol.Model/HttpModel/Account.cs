namespace ACME.Protocol.HttpModel
{
    public class Account
    {
        public string Status { get; set; }
        public string Orders { get; set; }

        public string[]? Contact { get; set; }
        public bool? TermsOfServiceAgreed { get; set; }
        public object? ExternalAccountBinding { get; set; }
    }
}
