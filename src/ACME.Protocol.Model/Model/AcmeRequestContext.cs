using System;
using System.Collections.Generic;
using System.Text;

namespace TG_IT.ACME.Server.Model
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
