using Microsoft.EntityFrameworkCore;
using PurchaseApi.Domain.entities;

namespace PurchaseApi.Infrastructure.DataAcess
{
    public class PurchaseDbContext : DbContext
    {
        public PurchaseDbContext(DbContextOptions<PurchaseDbContext> options) : base(options) { }
        public DbSet<Purchase> Purchase { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>()
                .HasIndex(p => p.Id)
                .IsUnique();
        }
    }
}
