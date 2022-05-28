using Microsoft.EntityFrameworkCore;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services;

namespace StockTrader.EntityFramework.Services
{
    public class AccountDataService : IAccountService
    {
        private readonly StockTraderDbContextFactory _contextFactory;

        public AccountDataService(StockTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Account> Create(Account entity)
        {
            using (StockTraderDbContext context = _contextFactory.CreateDbContext())
            {
                var createdEntity = await context.Set<Account>()
                    .AddAsync(entity);

                await context.SaveChangesAsync();

                return createdEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (StockTraderDbContext context = _contextFactory.CreateDbContext())
            {
                Account entity = await context.Set<Account>()
                    .FirstOrDefaultAsync((a) => a.Id == id);

                context.Set<Account>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<Account> Get(int id)
        {
            using (StockTraderDbContext context = _contextFactory.CreateDbContext())
            {
                Account entity = await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync((a) => a.Id == id);

                return entity;
            }
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using (StockTraderDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Account> entities = await context.Accounts
                    .Include(a=>a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .ToListAsync();

                return entities;
            }
        }

        public async Task<Account> GetByEmail(string email)
        {
            using (StockTraderDbContext context = _contextFactory.CreateDbContext())
            {
                Account entity = await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync((a) => a.AccountHolder.Email == email);

                return entity;
            }
        }

        public async Task<Account> GetByUsername(string username)
        {
            using (StockTraderDbContext context = _contextFactory.CreateDbContext())
            {
                Account entity = await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync((a) => a.AccountHolder.Username == username);

                return entity;
            }
        }

        public async Task<Account> Update(int id, Account entity)
        {
            using (StockTraderDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<Account>()
                    .Update(entity);

                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
