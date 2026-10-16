using MassTransit;
using PurchaseApi.Domain.Events;
using PurchaseApi.Domain.Services;

namespace PurchaseApi.Infrastructure.Messaging
{
    public class PurchaseEventPublisher : IPurchaseEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PurchaseEventPublisher(IPublishEndpoint publishEndpoint)
            => _publishEndpoint = publishEndpoint;

        public Task PublishPurchaseCreatedAsync(PurchaseCreatedEvent evt, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(evt);
            return _publishEndpoint.Publish(evt, ct);
        }
    }
}
