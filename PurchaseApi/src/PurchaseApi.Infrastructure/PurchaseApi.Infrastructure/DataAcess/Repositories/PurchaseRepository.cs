using PurchaseApi.Domain.entities;
using PurchaseApi.Domain.Repositories.Purchases;

namespace PurchaseApi.Infrastructure.DataAcess.Repositories
{
    internal class PurchaseRepository : IPurchaseWriteOnlyRepository
    {
        private readonly PurchaseDbContext _dbContext;
        public PurchaseRepository(PurchaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Purchase purchase)
        {
            await _dbContext.Purchase.AddAsync(purchase);
        }
    }
    }
}
