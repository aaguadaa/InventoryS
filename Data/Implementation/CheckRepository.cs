using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Data.Contracts;
using Domain.Model;

namespace Data.Repositories
{
    public class CheckRepository : ICheckRepository
    {
        private readonly InventoryStevDBContext _dbContext;

        public CheckRepository(InventoryStevDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Check>> GetAllAsync()
        {
            return await _dbContext.Set<Check>().ToListAsync();
        }
        public async Task<IEnumerable<Check>> GetChecksByDateAsync(DateTime date)
        {
            return await _dbContext.Set<Check>().Where(c => c.CreatedDate.Date == date.Date).ToListAsync();
        }
        public async Task<IEnumerable<Check>> GetChecksByMonthAndYearAsync(int month, int year)
        {
            return await _dbContext.Set<Check>().Where(c => c.CreatedDate.Month == month && c.CreatedDate.Year == year).ToListAsync();
        }
        public async Task<IEnumerable<Check>> GetChecksByYearAsync(int year)
        {
            return await _dbContext.Set<Check>().Where(c => c.CreatedDate.Year == year).ToListAsync();
        }
        public async Task AddNoteToCheckAsync(int checkId, string note)
        {
            var check = await _dbContext.Set<Check>().FindAsync(checkId);
            if (check != null)
            {
                check.Notes.Add(note);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<int> AddCheckAsync(Check entity)
        {
            _dbContext.Set<Check>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<bool> UpdateCheckAsync(Check entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCheckAsync(int id)
        {
            var check = await _dbContext.Set<Check>().FindAsync(id);
            if (check == null)
                return false;

            _dbContext.Set<Check>().Remove(check);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Check> GetByIdCheckAsync(int id)
        {
            return await _dbContext.Set<Check>().FindAsync(id);
        }
        public List<Check> GetChecksByAccountId(int accountId)
        {
            return _dbContext.Checks.Where(c => c.Account.Id == accountId).ToList();
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _dbContext.Products.FindAsync(productId);
        }
        public async Task<IEnumerable<Check>> GetChecksByMonthAsync(int month)
        {
            DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year;

            return await Task.Run(() =>
            {
                // Obtener los cortes por mes y año
                return _dbContext.Checks
                    .Where(c => c.CreatedDate.Month == month && c.CreatedDate.Year == currentYear)
                    .ToList();
            });
        }
    }
}
