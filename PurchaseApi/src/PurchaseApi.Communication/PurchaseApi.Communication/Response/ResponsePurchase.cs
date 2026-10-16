using PurchaseApi.Domain.Enuns;

namespace PurchaseApi.Communication.Response
{
    public class ResponsePurchase
    {
        public required string ItemID { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal Total { get; set; }
    }
}
