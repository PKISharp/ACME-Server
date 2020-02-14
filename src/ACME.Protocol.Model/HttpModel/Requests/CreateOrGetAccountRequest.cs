using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Protocol.HttpModel.Requests
{
    public class CreateOrGetAccount
    {
        public List<string> EmailAddress { get; set; }
        public bool AcceptTOS { get; set; }
    }
}
