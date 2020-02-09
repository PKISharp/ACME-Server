namespace ACME.Protocol.HttpModel
{
    public class Dns01Challenge : TokenChallenge
    {
        public Dns01Challenge()
        {
            Type = "dns-01";
        }
    }
}
