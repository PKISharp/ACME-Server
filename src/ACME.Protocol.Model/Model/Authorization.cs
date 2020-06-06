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

        internal void SelectChallenge(Challenge challenge)
        {
            _challenges?.RemoveAll(c => c != challenge);
        }


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
