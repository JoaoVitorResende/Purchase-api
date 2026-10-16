using PurchaseApi.Domain.entities;

namespace PurchaseApi.Domain.Repositories.Purchases
{
    public interface IPurchaseReadOnlyRepository
    {
        Task<List<Purchase>> GetAll();
    }
}
