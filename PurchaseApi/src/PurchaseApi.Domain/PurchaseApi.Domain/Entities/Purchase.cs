using PurchaseApi.Domain.Enuns;
using PurchaseApi.Exception.ExceptionBase;

namespace PurchaseApi.Domain.entities
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public required string ItemID { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total {  get; set; }
        public PurchaseStatus Status { get; private set; } = PurchaseStatus.Pending;
        public string? RejectionReason { get; private set; }

        public void Confirm()
        {
            if (Status == PurchaseStatus.Confirmed)
                return; // mensagem repetida: não faz nada

            if (Status == PurchaseStatus.Rejected)
                throw new PurchaseFailedException("Compra rejeitada não pode ser confirmada.");

            Status = PurchaseStatus.Confirmed;
        }

        public void Reject(string? reason)
        {
            if (Status == PurchaseStatus.Rejected)
                return;

            if (Status == PurchaseStatus.Confirmed)
                throw new PurchaseFailedException("Compra confirmada não pode ser rejeitada.");

            Status = PurchaseStatus.Rejected;
            RejectionReason = string.IsNullOrWhiteSpace(reason) ? "Motivo não informado." : reason;
        }
    }
}
