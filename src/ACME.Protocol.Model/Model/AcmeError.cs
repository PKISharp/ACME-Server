using System;
using System.Collections.Generic;
using System.Linq;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class AcmeError
    {
        private string? _type;
        private string? _detail;

        private AcmeError() { }

        public AcmeError(string type, string detail, Identifier identifier, IEnumerable<AcmeError>? subErrors = null)
        {
            Type = type;
            Detail = detail;
            Identifier = identifier;
            SubErrors = subErrors?.ToList();
        }

        public string Type { 
            get => _type ?? throw new NotInitializedException();
            private set => _type = value; 
        }
        
        public string Detail { 
            get => _detail ?? throw new NotInitializedException(); 
            set => _detail = value; 
        }

        public Identifier? Identifier { get; private set; }

        public List<AcmeError>? SubErrors
        {
            get;
            private set;
        }
    }
}
