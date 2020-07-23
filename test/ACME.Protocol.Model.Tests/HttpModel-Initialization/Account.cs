using System;
using System.Collections.Generic;
using Xunit;

namespace TGIT.ACME.Protocol.Model.Tests.HttpModel_Initialization
{
    public class Account
    {
        public static readonly string JwkJson = @"{ ""kty"" : ""RSA"", ""kid"" : ""cc34c0a0-bd5a-4a3c-a50d-a2a7db7643df"", ""n""   : ""pjdss8ZaDfEH6K6U7GeW2nxDqR4IP049fk1fK0lndimbMMVBdPv_hSpm8T8EtBDxrUdi1OHZfMhUixGaut-3nQ4GG9nM249oxhCtxqqNvEXrmQRGqczyLxuh-fKn9Fg--hS9UpazHpfVAFnB5aCfXoNhPuI8oByyFKMKaOVgHNqP5NBEqabiLftZD3W_lsFCPGuzr4Vp0YS7zS2hDYScC2oOMu4rGU1LcMZf39p3153Cq7bS2Xh6Y-vw5pwzFYZdjQxDn8x8BG3fJ6j8TGLXQsbKH1218_HcUJRvMwdpbUQG5nvA2GXVqLqdwp054Lzk9_B_f1lVrmOKuHjTNHq48w"", ""e""   : ""AQAB"" }";

        [Fact]
        public void Ctor_Initializes_All_Properties()
        {
            var account = new Model.Account(new Model.Jwk(JwkJson), new List<string> { "some@example.com", "other@example.com" }, DateTimeOffset.UtcNow);
            var ordersUrl = "https://orders.example.org/";

            var sut = new HttpModel.Account(account, ordersUrl);

            Assert.Equal(account.Contacts, sut.Contact);
            Assert.Equal(ordersUrl, sut.Orders);
            Assert.Equal(account.Status.ToString().ToLowerInvariant(), sut.Status);
            Assert.True(sut.TermsOfServiceAgreed);
        }

        [Fact]
        public void Empty_TOSAccepted_Will_Yield_False()
        {
            var account = new Model.Account(new Model.Jwk(JwkJson), new List<string> { "some@example.com", "other@example.com" }, null);
            var ordersUrl = "https://orders.example.org/";

            var sut = new HttpModel.Account(account, ordersUrl);

            Assert.False(sut.TermsOfServiceAgreed);
        }
    }
}
