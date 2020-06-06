using System;
using System.Collections.Generic;
using System.Linq;
using TGIT.ACME.Protocol.HttpModel;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Challenge
    {
        private static Dictionary<ChallengeStatus, ChallengeStatus[]> _validStatusTransitions = 
            new Dictionary<ChallengeStatus, ChallengeStatus[]>
            {
                { ChallengeStatus.Pending, new [] { ChallengeStatus.Processing } },
                { ChallengeStatus.Processing, new [] { ChallengeStatus.Processing, ChallengeStatus.Invalid, ChallengeStatus.Valid } }
            };

        private string? _challengeId;
        private string? _type;
        private string? _token;

        private Challenge() { }

        public Challenge(string type, string token)
        {
            ChallengeId = GuidString.NewValue();
            Type = type;
            Token = token;
        }

        public string ChallengeId {
            get => _challengeId ?? throw new NotInitializedException();
            private set => _challengeId = value;
        }

        public string Type {
            get => _type ?? throw new NotInitializedException();
            private set => _type = value;
        }

        public ChallengeStatus Status { get; private set; }

        public string Token {
            get => _token ?? throw new NotInitializedException();
            private set => _token = value; 
        }

        public HttpModel.AcmeError? Error { get; set; } //TODO: Probably change model to something else.
        
        public DateTimeOffset? Validated { get; set; }


        internal void SetStatus(ChallengeStatus nextStatus)
        {
            if (!_validStatusTransitions.ContainsKey(Status))
                throw new InvalidOperationException($"Cannot do challenge status transition from '{Status}'.");

            if (!_validStatusTransitions[Status].Contains(nextStatus))
                throw new InvalidOperationException($"Cannot do challenge status transition from '{Status}' to {nextStatus}.");

            Status = nextStatus;
        }
    }
}
