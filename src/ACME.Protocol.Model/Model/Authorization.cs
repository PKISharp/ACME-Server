using System;
using System.Collections.Generic;
using System.Linq;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Authorization
    {
        private string? _authorizationId;
        private Identifier? _identifier;
        private List<Challenge>? _challenges;

        private Authorization() { }

        public Authorization(Identifier identifier, IEnumerable<Challenge> challenges, DateTimeOffset expires)
        {
            AuthorizationId = GuidString.NewValue();
            Challenges = challenges.ToList();

            Identifier = identifier;
            IsWildcard = identifier.Value.StartsWith("*", StringComparison.InvariantCulture);

            Expires = expires;
        }

        public string AuthorizationId { 
            get => _authorizationId ?? throw new NotInitializedException(); 
            private set => _authorizationId = value; 
        }
        public AuthorizationStatus Status { get; private set; }

        public Identifier Identifier {
            get => _identifier ?? throw new NotInitializedException(); 
            private set => _identifier = value; 
        }
        public bool IsWildcard { get; private set; }

        public DateTimeOffset? Expires { get; private set; }

        public IReadOnlyCollection<Challenge> Challenges {
            get => _challenges ?? throw new NotInitializedException();
            private set => _challenges = value.ToList(); 
        }
        
        public Challenge? GetChallenge(string challengeId)
            => Challenges.FirstOrDefault(x => x.ChallengeId == challengeId);
    }
}
