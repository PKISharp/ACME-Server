using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Protocol.HttpModel.Requests
{
    public class AcmeRequestHeader
    {
        public string Alg { get; set; }
        public string Nonce { get; set; }
        public string Url { get; set; }

        public string Kid { get; set; }
        public string Jwk { get; set; }
    }

    public class AcmeHttpRequest
    {
        public AcmeRequestHeader Protected { get; set; }

        public string Signature { get; set; }
    }

    public class AcmeHttpRequest<TPayload> : AcmeHttpRequest
    {
        public TPayload Payload { get; set; }
    }
}
