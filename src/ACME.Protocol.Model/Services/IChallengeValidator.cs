using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
    public interface IChallangeValidatorFactory
    {
        IChallengeValidator GetValidator(Challenge challenge);
    }

    public interface IChallengeValidator
    {
        Task<bool> ValidateChallengeAsync(Challenge challenge, CancellationToken cancellationToken);
    }

    public class Dns01ChallangeValidator : IChallengeValidator
    { }

    public class Http01ChallangeValidator : IChallengeValidator
    { }
}
