using System;
using System.Runtime.CompilerServices;

namespace ACME.Protocol.Exceptions
{
    internal class NotInitializedException : InvalidOperationException
    {
        public NotInitializedException([CallerMemberName]string caller = null!)
            :base($"{caller} has been accessed before being initialized.")
        {

        }
    }
}
