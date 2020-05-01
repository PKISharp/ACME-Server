using System.Collections.Generic;

namespace TG_IT.ACME.Protocol.HttpModel
{
    public class Order
    {
        public string Status { get; set; }
        public string? Expires { get; set; }
        public List<Identifier> Identifiers { get; set; }
        public string? NotBefore { get; set; }
        public string? NotAfter { get; set; }
        public object? Error { get; set; }
        public List<string> Authorizations { get; set; }
        public string Finalize { get; set; }
        public string? Certificate { get; set; }
    }
}
