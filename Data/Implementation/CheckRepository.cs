using Data.Contracts;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class CheckRepository : ICheckRepository
    {
        private readonly InventoryStevDBContext _dbContext;

        public CheckRepository(InventoryStevDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Check entity)
        {
            if (entity == null) return 0;
            _dbContext.Checks.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }
        public bool Delete(int id)
        {
            Check entityToDelete = _dbContext.Checks.Find(id);
            if (entityToDelete == null) return false;
            _dbContext.Checks.Remove(entityToDelete);
            _dbContext.SaveChanges();
            return true;
        }
        public Check Get(int id)
        {
            return _dbContext.Checks.Find(id);
        }
        public IEnumerable<Check> GetAll(Expression<Func<Check, bool>> filter = null, Func<IQueryable<Check>, IOrderedQueryable<Check>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Check> query = _dbContext.Checks;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public Check GetById(int id, params Expression<Func<Check, object>>[] includeProperties)
        {
            IQueryable<Check> query = _dbContext.Checks;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault(c => c.Id == id);
        }
        public bool Update(Check entity)
        {
            var existingEntity = _dbContext.Checks.Find(entity.Id);
            if (existingEntity == null) return false;
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            _dbContext.SaveChanges();
            return true;
        }
        public async Task<IEnumerable<Check>> GetChecksByDate(DateTime date)
        {
            return await _dbContext.Checks.Where(c => c.CreatedDate.Date == date.Date).ToListAsync();
        }
        public async Task<IEnumerable<Check>> GetChecksByUserId(int userId)
        {
            return await _dbContext.Checks.Where(c => c.User.Id == userId).ToListAsync();
        }
        public async Task<IEnumerable<Check>> GetAllByDateAsync(DateTime date)
        {
            return await _dbContext.Checks
                                     .Where(c => c.CreatedDate.Date == date.Date)
                                     .ToListAsync();
        }
        public async Task<IEnumerable<Check>> GetAllByUserIdAsync(int userId)
        {
            return await _dbContext.Checks
                                     .Where(c => c.User.Id == userId)
                                     .ToListAsync();
        }
    }
}