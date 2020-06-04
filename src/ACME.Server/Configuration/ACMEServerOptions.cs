namespace TGIT.ACME.Server.Configuration
{
    public class ACMEServerOptions
    {
        public bool UseHostedServices { get; set; }

        public TOSOptions TOS { get; set; } = new TOSOptions();
        public string? WebsiteUrl { get; set; }
    }
}
