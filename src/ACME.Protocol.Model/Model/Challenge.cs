using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TGIT.ACME.Protocol.HttpModel;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public static class ChallengeTypes
    {
        public const string Http01 = "http-01";
        public const string Dns01 = "dns-01";

        public static readonly string[] AllTypes = new[] { Http01, Dns01 };
    }

    public class Challenge
    {
        private static readonly Dictionary<ChallengeStatus, ChallengeStatus[]> _validStatusTransitions = 
            new Dictionary<ChallengeStatus, ChallengeStatus[]>
            {
                { ChallengeStatus.Pending, new [] { ChallengeStatus.Processing } },
                { ChallengeStatus.Processing, new [] { ChallengeStatus.Processing, ChallengeStatus.Invalid, ChallengeStatus.Valid } }
            };

        private string? _challengeId;
        private Authorization? _authorization;

        private string? _type;
        private string? _token;

        public Challenge() 
        {
            ChallengeId = GuidString.NewValue();
        }

        public Challenge(Authorization authorization, string type, string token)
            :this()
        {
            if (!ChallengeTypes.AllTypes.Contains(type))
                throw new InvalidOperationException($"Unknown ChallengeType {type}");

            Type = type;
            Token = token;

            Authorization = authorization;
            Authorization.Challenges.Add(this);
        }

        public string ChallengeId {
            get => _challengeId ?? throw new NotInitializedException();
            set => _challengeId = value;
        }

        [DisallowNull]
        public Authorization Authorization {
            get => _authorization;
            set => _authorization = value;
        }

        public string Type {
            get => _type ?? throw new NotInitializedException();
            set => _type = value;
        }

        public ChallengeStatus Status { get; set; }

        public string Token {
            get => _token ?? throw new NotInitializedException();
            set => _token = value; 
        }

        public AcmeError? Error { get; set; }
        
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
