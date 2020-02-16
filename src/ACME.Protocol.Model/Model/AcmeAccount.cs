using System;
using System.Collections.Generic;

namespace ACME.Protocol.Model
{
    public class AcmeAccount
    {
        public AccountStatus Status { get; set; }

        public KeyWrapper Keys { get; set; }

        public List<string>? Contact { get; set; }

        public DateTimeOffset? TOSAccepted { get; set; }
        public Guid AccountId { get; internal set; }
    }
}
