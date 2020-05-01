namespace TG_IT.ACME.Protocol.HttpModel
{
    public class Challenge
    {
        public string Url { get; set; }
        public string Type { get; protected set; }
        public string Status { get; set; }

        public string? Validated { get; set; }
        public Error? Error { get; set; }
    }

    public class TokenChallenge : Challenge
    {
        public string Token { get; set; }
    }

    public class Dns01Challenge : TokenChallenge
    {
        public Dns01Challenge()
        {
            Type = "dns-01";
        }
    }

    public class Http01Challenge : TokenChallenge
    {
        public Http01Challenge()
        {
            Type = "http-01";
        }
    }
}
