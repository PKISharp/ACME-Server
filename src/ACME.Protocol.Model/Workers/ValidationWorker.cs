using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;
using TGIT.ACME.Protocol.Services;
using TGIT.ACME.Protocol.Storage;

namespace TGIT.ACME.Protocol.Workers
{
    public interface IValidationWorker
    {
        Task RunAsync(CancellationToken cancellationToken);
    }

    public class ValidationWorker : IValidationWorker
    {
        private readonly IOrderStore _orderStore;
        private readonly IChallangeValidatorFactory _challangeValidatorFactory;

        public ValidationWorker(IOrderStore orderStore, IChallangeValidatorFactory challangeValidatorFactory)
        {
            _orderStore = orderStore;
            _challangeValidatorFactory = challangeValidatorFactory;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderStore.GetValidatableOrders(cancellationToken);
            while(orders.Count != 0)
            {
                var tasks = new Task[orders.Count];
                for(int i = 0; i < orders.Count; ++i)
                    tasks[i] = ValidateOrder(orders[i], cancellationToken);

                Task.WaitAll(tasks, cancellationToken);

                if(!cancellationToken.IsCancellationRequested)
                    orders = await _orderStore.GetValidatableOrders(cancellationToken);
            }
        }

        private async Task ValidateOrder(Order order, CancellationToken cancellationToken)
        {
            var pendingAuthZs = order.Authorizations.Where(a => a.Challenges.Any(c => c.Status == ChallengeStatus.Processing));

            foreach(var pendingAuthZ in pendingAuthZs)
            {
                if(pendingAuthZ.Expires <= DateTimeOffset.UtcNow)
                {
                    pendingAuthZ.ClearChallenges();
                    pendingAuthZ.SetStatus(AuthorizationStatus.Expired);
                    continue;
                }

                var challenge = pendingAuthZ.Challenges[0];

                var validator = _challangeValidatorFactory.GetValidator(challenge);
                var isValid = await validator.ValidateChallengeAsync(challenge, cancellationToken);

                challenge.SetStatus(isValid ? ChallengeStatus.Valid : ChallengeStatus.Invalid);
                pendingAuthZ.SetStatus(isValid ? AuthorizationStatus.Valid : AuthorizationStatus.Invalid);
            }

            order.SetStatusFromAuthorizations();
            await _orderStore.SaveOrderAsync(order, cancellationToken);
        }
    }
}
