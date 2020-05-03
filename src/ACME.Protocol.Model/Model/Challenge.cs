namespace TG_IT.ACME.Protocol.Model
{
    public class Challenge
    {
        public Challenge()
        {
            ChallengeId = new GuidString();
        }

        public string ChallengeId { get; set; }

        public string Type { get; set; }
    }
}
