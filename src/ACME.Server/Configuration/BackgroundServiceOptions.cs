namespace TGIT.ACME.Server.Configuration
{
    public class BackgroundServiceOptions
    {
        public bool EnableValidationService { get; set; }
        public bool EnableIssuanceService { get; set; }

        public int ValidationCheckInterval { get; set; } = 300;
        public int IssuanceCheckInterval { get; set; } = 300;
    }
}
