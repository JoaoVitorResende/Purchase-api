using PurchaseApi.Domain.Repositories;

namespace PurchaseApi.Infrastructure.DataAcess.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly PurchaseDbContext _dbContext;

        public UnitOfWork(PurchaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
