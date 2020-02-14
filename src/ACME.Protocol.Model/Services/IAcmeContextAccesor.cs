using ACME.Protocol.Model;

namespace ACME.Protocol.Services
{
    public interface IAcmeContextAccesor
    {
        AcmeRequestContext Context { get; set; }
    }
}
