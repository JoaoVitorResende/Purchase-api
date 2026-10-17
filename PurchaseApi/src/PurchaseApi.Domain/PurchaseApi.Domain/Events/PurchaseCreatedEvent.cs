namespace Contracts.Events;
public record PurchaseCreatedEvent(Guid PurchaseId, string ItemId, decimal Quantity, decimal Total, DateTime CreatedAt);
