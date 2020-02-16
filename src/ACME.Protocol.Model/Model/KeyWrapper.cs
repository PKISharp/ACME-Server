using Microsoft.IdentityModel.Tokens;

namespace ACME.Protocol.Model
{
    public class KeyWrapper
    {
        public JsonWebKey Jwk { get; set; }
        public SecurityKey SecurityKey { get; set; }
    }
}
