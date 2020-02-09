namespace ACME.Protocol.HttpModel
{
    public class Error
    {
        public string Type { get; set; }
        public string Detail { get; set; }
        public SubError[]? Subproblems { get; set; }
    }
}
