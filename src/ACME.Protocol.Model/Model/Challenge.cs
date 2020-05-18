using System;
using TGIT.ACME.Protocol.HttpModel;

namespace TGIT.ACME.Protocol.Model
{
    public class Challenge
    {
        public Challenge()
        {
            ChallengeId = new GuidString();
        }

        public string ChallengeId { get; set; }

        public string Type { get; set; }
        public ChallengeStatus Status { get; set; }

        public Error? Error { get; set; } //TODO: Probably change model to something else.
        public DateTimeOffset? Validated { get; set; }
    }
}
