using Contracts.Events;

namespace PurchaseApi.Domain.Services
{
    public interface IPurchaseEventPublisher
    {
        Task PublishPurchaseCreatedAsync(PurchaseCreatedEvent evt, CancellationToken ct = default);
    }
}
