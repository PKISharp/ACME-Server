using System;
using System.Collections.Generic;
using System.Text;

namespace TG_IT.ACME.Protocol.HttpModel.Requests
{
    public class CreateOrder
    {
        public List<Identifier> Identifiers { get; set; }

        public DateTimeOffset? NotBefore { get; set; }
        public DateTimeOffset? NotAfter { get; set; }
    }
}
