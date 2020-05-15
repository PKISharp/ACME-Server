using System;
using System.Collections.Generic;
using System.Linq;

namespace TG_IT.ACME.Protocol.Model
{
    public class Authorization
    {
        public Authorization()
        {
            AuthorizationId = new GuidString();
            Challenges = new List<Challenge>();
        }

        public string AuthorizationId { get; set; }

        public Identifier Identifier { get; set; }
        public AuthorizationStatus Status { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public List<Challenge> Challenges { get; set; }
        public bool? IsWildcard { get; set; }

        public Challenge? GetChallenge(string challengeId)
            => Challenges.FirstOrDefault(x => x.ChallengeId == challengeId);
    }
}
