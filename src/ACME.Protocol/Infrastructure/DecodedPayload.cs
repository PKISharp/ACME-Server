namespace TGIT.ACME.Protocol.Infrastructure
{
    public class DecodedPayload
    {
        public DecodedPayload(string encodedJson)
        {
            EncodedJson = encodedJson;
        }

        public string EncodedJson { get; }
    }

    public class DecodedPayload<TPayload> : DecodedPayload
        where TPayload : class?
    {
        public DecodedPayload(string encodedJson, TPayload value)
            : base(encodedJson)
        {
            Value = value;
        }

        public TPayload Value { get; set; }
    }
}
