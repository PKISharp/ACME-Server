using TGIT.ACME.Server.Model;

namespace TGIT.ACME.Protocol.Services
{
    public interface IAcmeContextAccesor
    {
        AcmeRequestContext Context { get; set; }
    }
}
