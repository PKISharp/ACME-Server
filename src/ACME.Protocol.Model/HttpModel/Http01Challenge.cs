namespace ACME.Protocol.HttpModel
{
    public class Http01Challenge: TokenChallenge
    {
        public Http01Challenge()
        {
            Type = "http-01";
        }
    }
}
