using System;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Server.Model
{
    public class AcmeRequestContext
    {
        public AcmeRequestContext(IServiceProvider services)
        {
            Services = services;
        }

        public IServiceProvider Services { get; }
        
        public Nonce Nonce { get; internal set; }
    }
}
