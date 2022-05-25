using Microsoft.EntityFrameworkCore;
using StockTrader.Domain.Models;

namespace StockTrader.EntityFramework
{
    public class StockTraderDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AssetTransaction> AssetTransactions { get; set; }

        public StockTraderDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetTransaction>()
                .OwnsOne(a => a.Stock);

            base.OnModelCreating(modelBuilder);
        }
    }
}
