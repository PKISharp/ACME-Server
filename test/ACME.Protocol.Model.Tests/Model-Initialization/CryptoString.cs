using Xunit;

namespace TGIT.ACME.Protocol.Model.Tests.Model_Initialization
{
    public class CryptoString
    {
        [Fact]
        public void CryptoString_Seems_Filled()
        {
            var sut = Model.CryptoString.NewValue(48);

            Assert.False(string.IsNullOrWhiteSpace(sut));
            Assert.Equal(64, sut.Length);
        }
    }
}
