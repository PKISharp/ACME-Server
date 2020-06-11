using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Account : IVersioned
    {
        private string? _accountId;
        private Jwk? _jwk;

        private Account() { }

        public Account(Jwk jwk, IEnumerable<string>? contacts, bool tosAccepted)
        {
            AccountId = GuidString.NewValue();

            Jwk = jwk;
            Contacts = contacts?.ToList();
            if (tosAccepted)
                TOSAccepted = DateTimeOffset.UtcNow;
        }

        public string AccountId { 
            get => _accountId ?? throw new NotInitializedException(); 
            private set => _accountId = value; 
        }

        public AccountStatus Status { get; set; }

        public Jwk Jwk {
            get => _jwk ?? throw new NotInitializedException();
            private set => _jwk = value; 
        }

        public List<string>? Contacts { get; private set; }

        public DateTimeOffset? TOSAccepted { get; set; }

        /// <summary>
        /// Concurrency Token
        /// </summary>
        public long Version { get; set; }
    }
}
