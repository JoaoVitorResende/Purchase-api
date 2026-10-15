using PurchaseApi.Domain.Enuns;

namespace PurchaseApi.Domain.entities
{
    public class Purchase
    {
        public long Id { get; set; }
        public required string itemID { get; set; }
        public PaymentType paymentType { get; set; }
    }
}
