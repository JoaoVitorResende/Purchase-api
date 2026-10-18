using Microsoft.EntityFrameworkCore;
using PurchaseApi.Domain.entities;
using PurchaseApi.Domain.Repositories.Purchases;

namespace PurchaseApi.Infrastructure.DataAcess.Repositories
{
    internal class PurchaseRepository : IPurchaseWriteOnlyRepository, IPurchaseReadOnlyRepository
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

        public async Task<List<Purchase>> GetAll()
        {
            return await _dbContext.Purchase
              .AsNoTracking()
              .OrderBy(p => p.Id)
              .ToListAsync();
        }

        public async Task<Purchase?> GetById(Guid id)
        {
            return await _dbContext.Purchase
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
