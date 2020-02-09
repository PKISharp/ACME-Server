namespace ACME.Protocol.HttpModel
{
    public class Challenge
    {
        public string Url { get; set; }
        public string Type { get; protected set; }
        public string Status { get; set; }

        public string? Validated { get; set; }
        public Error? Error { get; set; }
    }
}
