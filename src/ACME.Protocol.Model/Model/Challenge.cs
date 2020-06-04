using System;
using TGIT.ACME.Protocol.HttpModel;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Challenge
    {
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
        public ChallengeStatus Status { get; set; }

        public string Token {
            get => _token ?? throw new NotInitializedException();
            private set => _token = value; 
        }

        public HttpModel.AcmeError? Error { get; set; } //TODO: Probably change model to something else.
        
        public DateTimeOffset? Validated { get; set; }
    }
}
