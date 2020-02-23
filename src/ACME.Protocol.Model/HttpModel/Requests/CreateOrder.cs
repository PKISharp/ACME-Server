﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Protocol.HttpModel.Requests
{
    public class CreateOrder
    {
        public Identifier[]? Identifiers { get; set; }

        public DateTimeOffset? NotBefore { get; set; }
        public DateTimeOffset? NotAfter { get; set; }
    }
}