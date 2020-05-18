using System;

namespace TGIT.ACME.Protocol.Model.Exceptions
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
}
