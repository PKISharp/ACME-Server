using System;

namespace TGIT.ACME.Protocol.Model.Exceptions
{
    internal class ConcurrencyException : InvalidOperationException
    {
        public ConcurrencyException()
            : base($"Object has been changed since loading")
        { }
    }
}
