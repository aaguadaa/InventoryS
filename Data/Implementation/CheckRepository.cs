using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Contracts;
using Domain.Model;

namespace Data.Implementation
{
    public class CheckRepository : ICheckRepository
    {
        public int Add(Check entity)
        {
            if (entity == null) return 0;
            using (var ctx = new InventoryStevDBContext())
            {
                ctx.Checks.Add(entity);
                ctx.SaveChanges();
                return entity.Id;
            }
        }
        public Check GetById(int id, params Expression<Func<Check, object>>[] includeProperties)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                IQueryable<Check> query = ctx.Checks;
                foreach (Expression<Func<Check, object>> includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.FirstOrDefault(x => x.Id == id);
            }
        }
        public void Update(Check entity)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Check Get(int id)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                return ctx.Checks.FirstOrDefault(x => x.Id == id);
            }
        }
        public IEnumerable<Check> GetAll(Expression<Func<Check, bool>> filter = null, Func<IQueryable<Check>, IOrderedQueryable<Check>> orderBy = null, string includeProperties = "")
        {
            using (var ctx = new InventoryStevDBContext())
            {
                IQueryable<Check> query = ctx.Checks;
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
        }
        public async Task<IEnumerable<Check>> GetChecksByMonthAsync(int month)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                return await ctx.Checks.Where(c => c.Date.Month == month).ToListAsync();
            }
        }
        public async Task<IEnumerable<Check>> GetChecksByDateAsync(DateTime date)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                return await ctx.Checks.Where(c => c.Date.Date == date.Date).ToListAsync();
            }
        }
        public async Task<IEnumerable<Check>> GetChecksByMonthAndYearAsync(int month, int year)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                return await ctx.Checks.Where(c => c.Date.Month == month && c.Date.Year == year).ToListAsync();
            }
        }
        public async Task<IEnumerable<Check>> GetChecksByYearAsync(int year)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                return await ctx.Checks.Where(c => c.Date.Year == year).ToListAsync();
            }
        }
        public async Task AddNoteToCheckAsync(int checkId, string note)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                var check = await ctx.Checks.FindAsync(checkId);
                if (check != null)
                {
                    check.Notes.Add(note);
                    await ctx.SaveChangesAsync();
                }
            }
        }
        public List<Check> GetChecksByAccountId(int accountId)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                return ctx.Checks.Where(c => c.AccountId == accountId).ToList();
            }
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                return await ctx.Products.FindAsync(productId);
            }
        }
        public async Task AddProductToCheckAsync(int checkId, Product product)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                var check = await ctx.Checks.FindAsync(checkId);
                if (check != null)
                {
                    check.Products.Add(product);
                    await ctx.SaveChangesAsync();
                }
            }
        }
        public async Task UpdateProductStatusAsync(int productId, string status)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                var product = await ctx.Products.FindAsync(productId);
                if (product != null)
                {
                    product.Status = status;
                    await ctx.SaveChangesAsync();
                }
            }
        }

        int IGenericRepository<Check>.Add(Check entity)
        {
            throw new NotImplementedException();
        }

        bool IGenericRepository<Check>.Update(Check entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Check>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
