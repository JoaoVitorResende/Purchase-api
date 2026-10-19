using PurchaseApi.Domain.Enuns;

namespace PurchaseApi.Domain.Messages
{
    public record PurchaseStatusChangedMessage(
    Guid PurchaseId,
    PurchaseStatus Status,
    string? Reason,
    DateTime OccurredAt);
}
