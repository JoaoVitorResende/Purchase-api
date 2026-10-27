using PurchaseApi.Domain.Enuns;

namespace PurchaseApi.Communication.Response
{
    public class ResponsePurchase
    {
        public Guid Id { get; set; }
        public required string ItemID { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
        public PurchaseStatus Status { get; set; }
        public string? RejectionReason { get; set; }
    }
}
