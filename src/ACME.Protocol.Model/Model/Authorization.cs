using System;
using System.Collections.Generic;
using System.Linq;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Authorization
    {
        private static Dictionary<AuthorizationStatus, AuthorizationStatus[]> _validStatusTransitions =
            new Dictionary<AuthorizationStatus, AuthorizationStatus[]>
            {
                { AuthorizationStatus.Pending, new [] { AuthorizationStatus.Invalid, AuthorizationStatus.Expired, AuthorizationStatus.Valid } },
                { AuthorizationStatus.Valid, new [] { AuthorizationStatus.Revoked, AuthorizationStatus.Deactivated, AuthorizationStatus.Expired } }
            };

        private string? _authorizationId;
        private Identifier? _identifier;
        private Order? _parent;

        private Authorization() {
            Challenges = new List<Challenge>();
        }

        public Authorization(Order parent, Identifier identifier, DateTimeOffset expires)
        {
            AuthorizationId = GuidString.NewValue();
            Challenges = new List<Challenge>();
            
            Parent = parent;
            Parent.Authorizations.Add(this);

            Identifier = identifier;
            IsWildcard = identifier.Value.StartsWith("*", StringComparison.InvariantCulture);

            Expires = expires;
        }

        public string AuthorizationId { 
            get => _authorizationId ?? throw new NotInitializedException(); 
            private set => _authorizationId = value; 
        }
        public AuthorizationStatus Status { get; private set; }

        public Order Parent {
            get => _parent ?? throw new NotInitializedException();
            private set => _parent = value;
        }
        
        public Identifier Identifier {
            get => _identifier ?? throw new NotInitializedException(); 
            private set => _identifier = value; 
        }
        
        public bool IsWildcard { get; private set; }

        public DateTimeOffset? Expires { get; private set; }

        
        public List<Challenge> Challenges { get; private set; }
        

        public Challenge? GetChallenge(string challengeId)
            => Challenges.FirstOrDefault(x => x.ChallengeId == challengeId);

        internal void SelectChallenge(Challenge challenge)
            => Challenges.RemoveAll(c => c != challenge);

        internal void ClearChallenges()
            => Challenges.Clear();


        internal void SetStatus(AuthorizationStatus nextStatus)
        {
            if (!_validStatusTransitions.ContainsKey(Status))
                throw new InvalidOperationException($"Cannot do challenge status transition from '{Status}'.");

            if (!_validStatusTransitions[Status].Contains(nextStatus))
                throw new InvalidOperationException($"Cannot do challenge status transition from '{Status}' to {nextStatus}.");

            Status = nextStatus;
        }
    }
}
