namespace TGIT.ACME.Protocol.Infrastructure
{
    public class DecodedHeader
    {
        public DecodedHeader(string encodedJson, AcmeHeader value)
        {
            EncodedJson = encodedJson;
            Value = value;
        }

        public string EncodedJson { get; }
        public AcmeHeader Value { get; }
    }
}
