using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TGIT.ACME.Protocol.Model.Tests.Model_Initialization
{
    public class Account
    {
        [Fact]
        public void Ctor_Populates_All_Properties()
        {
            var jwk = new Model.Jwk(StaticTestData.JwkJson);
            var contacts = new List<string> { "some@example.com" };
            var tosAccepted = DateTimeOffset.UtcNow;

            var sut = new Model.Account(jwk, contacts, tosAccepted);

            Assert.Equal(jwk, sut.Jwk);
            Assert.Equal(contacts, sut.Contacts);
            Assert.Equal(tosAccepted, sut.TOSAccepted);

            Assert.True(sut.AccountId.Length > 0);
            Assert.Equal(AccountStatus.Valid, sut.Status);
        }
    }

    public class Authorization
    {
        
    }

    public class Challenge
    {

    }

    public class Order
    {

    }
}
