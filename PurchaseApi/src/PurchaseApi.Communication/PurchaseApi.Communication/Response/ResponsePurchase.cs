using PurchaseApi.Domain.Enuns;

namespace PurchaseApi.Communication.Response
{
    public class ResponsePurchase
    {
        public required string itemID { get; set; }
        public PaymentType paymentType { get; set; }
    }
}
