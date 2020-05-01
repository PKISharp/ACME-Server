using System;

namespace TG_IT.ACME.Protocol.HttpModel
{
    public class Directory
    {
        public string NewNonce { get; set; }
        public string NewAccount { get; set; }
        public string NewOrder { get; set; }
        public string? NewAuthz { get; set; }
        public string RevokeCert { get; set; }
        public string KeyChange { get; set; }

        public DirectoryMetadata? Meta { get; set; }
    }
}
