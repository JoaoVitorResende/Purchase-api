using PurchaseApi.Domain.entities;

namespace PurchaseApi.Domain.Repositories.Purchases
{
    public interface IPurchaseWriteOnlyRepository
    {
        public Task Add(Purchase purchase);
    }
}
