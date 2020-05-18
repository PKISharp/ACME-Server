using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Account
    {
        private Jwk? _jwk;

        public Account()
        {
            AccountId = new GuidString();
        }

        public string AccountId { get; internal set; }

        public AccountStatus Status { get; set; }

        public Jwk Jwk {
            get => _jwk ?? throw new NotInitializedException();
            set => _jwk = value; 
        }

        public List<string>? Contact { get; set; }

        public DateTimeOffset? TOSAccepted { get; set; }

        /// <summary>
        /// Concurrency Token
        /// </summary>
        public long Version { get; set; }
    }
}
