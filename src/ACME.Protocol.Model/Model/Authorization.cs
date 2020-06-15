using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Authorization
    {
        private static readonly Dictionary<AuthorizationStatus, AuthorizationStatus[]> _validStatusTransitions =
            new Dictionary<AuthorizationStatus, AuthorizationStatus[]>
            {
                { AuthorizationStatus.Pending, new [] { AuthorizationStatus.Invalid, AuthorizationStatus.Expired, AuthorizationStatus.Valid } },
                { AuthorizationStatus.Valid, new [] { AuthorizationStatus.Revoked, AuthorizationStatus.Deactivated, AuthorizationStatus.Expired } }
            };

        private string? _authorizationId;
        private Identifier? _identifier;
        private Order? _order;

        public Authorization() {
            AuthorizationId = GuidString.NewValue();
            Challenges = new List<Challenge>();
        }

        public Authorization(Order order, Identifier identifier, DateTimeOffset expires)
            :this()
        {
            Order = order;
            Order.Authorizations.Add(this);

            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            Expires = expires;
        }

        public string AuthorizationId { 
            get => _authorizationId ?? throw new NotInitializedException(); 
            set => _authorizationId = value; 
        }
        public AuthorizationStatus Status { get; set; }

        [DisallowNull]
        public Order Order {
            get => _order;
            set => _order = value;
        }
        
        [DisallowNull]
        public Identifier Identifier {
            get => _identifier; 
            set => _identifier = value; 
        }

        public bool IsWildcard => Identifier.IsWildcard;

        public DateTimeOffset Expires { get; set; }

        
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
