using System;

namespace TGIT.ACME.Server.Configuration
{
    public class ACMEServerOptions
    {
        public TOSOptions TOS { get; set; } = new TOSOptions();
        public string WebsiteUrl { get; internal set; }
    }

    public class TOSOptions
    {
        public bool RequireAgreement { get; set; }
        public string Url { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
