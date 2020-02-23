namespace ACME.Protocol.Model
{
    public class Authorization
    {
        public string AuthorizationId { get; set; }

        public Identifier Identifier { get; set; }
        public string Status { get; set; }
        public string? Expires { get; set; }
        public Challenge[] Challenges { get; set; }
        public bool? Wildcard { get; set; }
    }
}
