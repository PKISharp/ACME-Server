using System.Collections.Generic;

namespace ACME.Protocol.HttpModel
{
    public class Error
    {
        public string Type { get; set; }
        public string Detail { get; set; }
        public List<SubError>? Subproblems { get; set; }
    }
}
