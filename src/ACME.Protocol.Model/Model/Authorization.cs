using System;
using System.Collections.Generic;

namespace TG_IT.ACME.Protocol.Model
{
    public class Authorization
    {
        public Authorization()
        {
            AuthorizationId = new GuidString();
        }

        public string AuthorizationId { get; set; }

        public Identifier Identifier { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public List<Challenge> Challenges { get; set; }
        public bool? IsWildcard { get; set; }
    }
}
