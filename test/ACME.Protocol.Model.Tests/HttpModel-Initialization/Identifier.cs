using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TGIT.ACME.Protocol.Model.Tests.HttpModel_Initialization
{
    public class Identifier
    {
        [Fact]
        public void Ctor_Intializes_All_Properties()
        {
            var identifier = new Model.Identifier("dns", "www.example.com");
            var sut = new HttpModel.Identifier(identifier);

            Assert.Equal(identifier.Type, sut.Type);
            Assert.Equal(identifier.Value, sut.Value);
        }
    }
}
