using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Domain.Model;

namespace Data.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly InventoryStevDBContext _dbContext;

        public AccountRepository(InventoryStevDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Account entity)
        {
            _dbContext.Set<Account>().Add(entity);
            return _dbContext.SaveChanges();
        }
        public bool Delete(int id)
        {
            var account = _dbContext.Set<Account>().Find(id);
            if (account != null)
            {
                _dbContext.Set<Account>().Remove(account);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public Account Get(int id)
        {
            return _dbContext.Set<Account>().Find(id);
        }
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _dbContext.Set<Account>().ToListAsync();
        }
        public async Task<IEnumerable<Account>> GetAllByNameAsync(string name)
        {
            return await _dbContext.Set<Account>().Where(a => a.Name.Contains(name)).ToListAsync();
        }
        public bool Update(Account entity)
        {
            var account = _dbContext.Set<Account>().Find(entity.Id);
            if (account != null)
            {
                _dbContext.Entry(account).CurrentValues.SetValues(entity);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        Task<bool> IAccountRepository.Update(Account entity)
        {
            return Task.FromResult(Update(entity));
        }

    }
}
