using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StockTrader.EntityFramework
{
    public class StockTraderDbContextFactory : IDesignTimeDbContextFactory<StockTraderDbContext>
    {
        public StockTraderDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<StockTraderDbContext> options = new DbContextOptionsBuilder<StockTraderDbContext>();

            options.UseSqlServer("Server=(local); Database=StockTraderDB; Trusted_Connection=True;");

            return new StockTraderDbContext(options.Options);
        }
    }
}
