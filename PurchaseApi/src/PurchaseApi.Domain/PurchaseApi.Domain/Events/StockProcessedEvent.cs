namespace Contracts.Events;

public record StockProcessedEvent(Guid PurchaseId, bool Succeeded, string? FailureReason);

