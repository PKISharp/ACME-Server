using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Protocol.HttpModel.Requests
{
    public class CreateOrder
    {
        public List<Identifier> Identifiers { get; } = new List<Identifier>();

        public DateTimeOffset? NotBefore { get; set; }
        public DateTimeOffset? NotAfter { get; set; }
    }
}
