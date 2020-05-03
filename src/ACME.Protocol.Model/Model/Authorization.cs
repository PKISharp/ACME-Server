namespace TG_IT.ACME.Protocol.Model
{
    public class Authorization
    {
        public Authorization()
        {
            AuthorizationId = new GuidString();
        }

        public string AuthorizationId { get; set; }

        public Identifier Identifier { get; set; }
        public string Status { get; set; }
        public string? Expires { get; set; }
        public Challenge[] Challenges { get; set; }
        public bool? IsWildcard { get; set; }
    }
}
