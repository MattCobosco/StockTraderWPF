using Microsoft.EntityFrameworkCore;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services;

namespace StockTrader.EntityFramework.Services
{
    /// <summary>
    /// CRUD for Account. Implementation of IAccountService.
    /// </summary>
    public class AccountDataService : IAccountService
    {
        private readonly StockTraderDbContextFactory _contextFactory;

        public AccountDataService(StockTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Create a new account.
        /// </summary>
        /// <param name="entity">A new account to be added to a database.</param>
        /// <returns>Added account.</returns>
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

        /// <summary>
        /// Delete an account by id.
        /// </summary>
        /// <param name="id">Id of the account to be deleted.</param>
        /// <returns>Boolean representing the deletion success/failure.</returns>
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

        /// <summary>
        /// Get an account by id.
        /// </summary>
        /// <param name="id">Id of the account.</param>
        /// <returns>Account matching the id. Can be null.</returns>
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

        /// <summary>
        /// Get all accounts.
        /// </summary>
        /// <returns>All accounts in the database.</returns>
        public async Task<IEnumerable<Account>> GetAll()
        {
            using (StockTraderDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Account> entities = await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .ToListAsync();

                return entities;
            }
        }

        /// <summary>
        /// Get account by email.
        /// </summary>
        /// <param name="email">Email of the account.</param>
        /// <returns>Account matching the email. Can be null.</returns>
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

        /// <summary>
        /// Get account by username.
        /// </summary>
        /// <param name="username">Username of the account.</param>
        /// <returns>
        /// Account matching the username. Can be null.</returns>
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

        /// <summary>
        /// Update an account.
        /// </summary>
        /// <param name="id">Id of the account to be updated.</param>
        /// <param name="entity">Updated account.</param>
        /// <returns>Updated account.</returns>
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
