namespace PurchaseApi.Domain.Events
{
    public record PurchaseCreatedEvent(Guid PurchaseId, decimal Total, DateTime CreatedAt);
}
