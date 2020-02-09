namespace ACME.Protocol.HttpModel
{
    public class Order
    {
        public string Status { get; set; }
        public string? Expires { get; set; }
        public string[] Identifiers { get; set; }
        public string? NotBefore { get; set; }
        public string? NotAfter { get; set; }
        public object? Error { get; set; }
        public string[] Authorizations { get; set; }
        public string Finalize { get; set; }
        public string? Certificate { get; set; }
    }
}
