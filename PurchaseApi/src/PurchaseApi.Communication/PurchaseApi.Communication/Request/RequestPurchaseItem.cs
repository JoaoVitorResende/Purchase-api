using PurchaseApi.Domain.Enuns;

namespace PurchaseApi.Communication.Request
{
    public class RequestPurchaseItem
    {
        public required string itemID { get; set; }
        public PaymentType paymentType { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
