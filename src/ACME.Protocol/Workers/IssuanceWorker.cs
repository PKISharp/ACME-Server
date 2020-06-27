using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.Model;
using TGIT.ACME.Protocol.Services;
using TGIT.ACME.Protocol.Storage;

namespace TGIT.ACME.Protocol.Workers
{

    public class IssuanceWorker : IIssuanceWorker
    {
        private readonly IOrderStore _orderStore;
        private readonly ICertificateIssuer _issuer;

        public IssuanceWorker(IOrderStore orderStore, ICertificateIssuer issuer)
        {
            _orderStore = orderStore;
            _issuer = issuer;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderStore.GetFinalizableOrders(cancellationToken);

            var tasks = new Task[orders.Count];
            for (int i = 0; i < orders.Count; ++i)
                tasks[i] = IssueCertificate(orders[i], cancellationToken);

            Task.WaitAll(tasks, cancellationToken);
        }

        private async Task IssueCertificate(Order order, CancellationToken cancellationToken)
        {
            var (certificate, error) = await _issuer.IssueCertificate(order.CertificateSigningRequest!, cancellationToken);

            if (certificate == null)
            {
                order.SetStatus(OrderStatus.Invalid);
                order.Error = error;
            }
            else if (certificate != null)
            {
                return;

                //TODO: failed Certificate issuance should invalidate the order.
                order.Certificate = certificate;
                order.SetStatus(OrderStatus.Valid);
            }

            await _orderStore.SaveOrderAsync(order, cancellationToken);
        }
    }
}
