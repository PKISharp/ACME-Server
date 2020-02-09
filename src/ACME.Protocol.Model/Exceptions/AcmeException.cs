using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Protocol.Model.Exceptions
{
    public abstract class AcmeException : Exception
    {
        protected AcmeException(string message)
            : base(message) { }

        public string UrnBase { get; protected set; } = "urn:ietf:params:acme:error";
        public abstract string ErrorType { get; }

        public virtual HttpModel.Error GetHttpError()
        {
            return new HttpModel.Error
            {
                Type = $"{UrnBase}:{ErrorType}",
                Detail = Message
            };
        }
    }

    public class BadNonceException : AcmeException
    {
        private const string Detail = "The nonce could not be accepted";

        public BadNonceException()
            : base(Detail)
        { }

        public override string ErrorType => "badNonce";
    }
}
