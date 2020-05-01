using System.Collections.Generic;

namespace TG_IT.ACME.Protocol.HttpModel
{
    public class Authorization
    {
        public Identifier Identifier { get; set; }
        public string Status { get; set; }
        public string? Expires { get; set; }
        public List<Challenge> Challanges { get; set; }
        public bool? Wildcard { get; set; }
    }
}
