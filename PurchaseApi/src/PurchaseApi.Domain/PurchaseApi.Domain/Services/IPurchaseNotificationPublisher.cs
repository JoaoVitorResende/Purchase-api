using PurchaseApi.Domain.Messages;

namespace PurchaseApi.Domain.Services
{
    public interface IPurchaseNotificationPublisher
    {
        Task Publish(PurchaseStatusChangedMessage message);
    }
}
