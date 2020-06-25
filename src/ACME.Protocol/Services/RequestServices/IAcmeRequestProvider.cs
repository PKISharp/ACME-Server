using TGIT.ACME.Protocol.HttpModel.Requests;

namespace TGIT.ACME.Protocol.Services.RequestServices
{
    public interface IAcmeRequestProvider
    {
        void Initialize(AcmeRawPostRequest rawPostRequest);

        AcmeRawPostRequest GetRequest();

        AcmeHeader GetHeader();
        
        TPayload GetPayload<TPayload>();
    }
}
