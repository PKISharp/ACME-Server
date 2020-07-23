using Xunit;

namespace TGIT.ACME.Protocol.Model.Tests.Model_Initialization
{
    public class GuidString
    {
        [Fact]
        public void GuidString_Seems_Filled()
        {
            var sut = Model.GuidString.NewValue();

            Assert.False(string.IsNullOrWhiteSpace(sut));
            Assert.Equal(22, sut.Length);
        }
    }
}
