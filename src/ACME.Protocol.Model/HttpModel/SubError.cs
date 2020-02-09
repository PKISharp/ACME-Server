namespace ACME.Protocol.HttpModel
{
    public class SubError
    {
        public string Type { get; set; }
        public string Detail { get; set; }
        public Identifier? Identifier { get; set; }
    }
}
