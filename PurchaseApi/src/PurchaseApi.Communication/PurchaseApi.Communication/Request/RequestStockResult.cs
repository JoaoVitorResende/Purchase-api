namespace PurchaseApi.Communication.Requests;

public record RequestStockResult(
    Guid PurchaseId,
    bool Succeeded,
    string? FailureReason);
