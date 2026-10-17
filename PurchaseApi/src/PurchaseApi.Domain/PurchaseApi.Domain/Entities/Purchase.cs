using PurchaseApi.Domain.Enuns;

namespace PurchaseApi.Domain.entities
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public required string ItemID { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total {  get; set; }
    }
}
