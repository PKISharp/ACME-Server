using System;

namespace TGIT.ACME.Protocol.Infrastructure
{
    public class AcmePostRequest
    {
        protected AcmePostRequest(DecodedHeader header, string signature)
        {
            if (string.IsNullOrWhiteSpace(signature))
                throw new ArgumentException("Request body signature is missing", nameof(signature));

            Header = header ?? throw new ArgumentNullException(nameof(header));
            Signature = signature;
        }

        public AcmePostRequest(DecodedHeader header, string? payload, string signature)
            :this(header, signature)
        {
            Payload = new DecodedPayload(payload ?? "");
        }

        public DecodedHeader Header { get; private set; }
        public DecodedPayload Payload { get; protected set; }

        public string Signature { get; private set; }
    }

    public class AcmePostRequest<TPayload> : AcmePostRequest
        where TPayload : class?
    {
        public AcmePostRequest(DecodedHeader header, DecodedPayload<TPayload> payload, string signature)
            : base(header, signature)
        {
            Payload = payload;
        }

        public new DecodedPayload<TPayload> Payload { 
            get => (DecodedPayload<TPayload>)base.Payload; 
            private set => base.Payload = value; 
        }
    }
}
