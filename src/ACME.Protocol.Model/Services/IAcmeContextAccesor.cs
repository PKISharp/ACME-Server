using TG_IT.ACME.Server.Model;

namespace TG_IT.ACME.Protocol.Services
{
    public interface IAcmeContextAccesor
    {
        AcmeRequestContext Context { get; set; }
    }
}
